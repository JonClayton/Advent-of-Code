from python.solutions.classes import Solution
from python.solutions.utilities import convert_string_list_to_ints


def first_solution(lines):
    return solve_challenge(lines, False)


def second_solution(lines):
    return solve_challenge(lines, True)


# LOL the "grid" is a dictionary because we don't care about the cells that no line touches!
# not so LOL, python forces you to use a dictionary when all you want is a hash table
def solve_challenge(lines, is_diagonal_allowed=False):
    seen = {}
    seen_twice = {}
    for line_endpoints in convert_lines_to_line_endpoints_list(lines):
        for point in get_points_from_line_endpoints(line_endpoints, is_diagonal_allowed):
            if point in seen:
                if point not in seen_twice:
                    seen_twice.update({point: True})
            else:
                seen.update({point: True})
    return len(seen_twice.keys())


solution = Solution('../inputs/inputs_5.json', first_solution, second_solution)


def add_increment_to_point(line_endpoints, increment):
    x0, y0 = line_endpoints
    x1, y1 = increment
    return x0 + x1, y0 + y1


def convert_lines_to_line_endpoints_list(lines):
    result = []
    for line in lines:
        two_points_string = line.split(' -> ')
        p0 = convert_string_list_to_ints(two_points_string[0].split(','))
        p1 = convert_string_list_to_ints(two_points_string[1].split(','))
        result.append(((p0[0], p0[1]), (p1[0], p1[1])))
    return result


def get_points_from_line_endpoints(line_endpoints, is_diagonal_allowed):
    result = []
    x1, y1 = line_endpoints[0]
    (increment, count) = get_slope_increment_and_count(line_endpoints, is_diagonal_allowed)
    current = x1, y1
    for index in range(count):
        result.append(current)
        current = add_increment_to_point(current, increment)
    return result


def get_slope_increment_and_count(line_endpoints, is_diagonal_allowed):
    (x1, y1), (x2, y2) = line_endpoints
    if x1 == x2:
        length = abs(y2 - y1)
        increment = (0, (y2 - y1) // length)
    elif y1 == y2:
        length = abs(x2 - x1)
        increment = ((x2 - x1) // length, 0)
    elif is_diagonal_allowed:
        length = abs(x2 - x1)
        increment = ((x2 - x1) // length, (y2 - y1) // length)
    else:
        return None, 0
    return increment, length + 1
