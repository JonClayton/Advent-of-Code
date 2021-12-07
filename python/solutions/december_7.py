from python.solutions.classes import Solution
from python.solutions.utilities import convert_string_list_to_ints


def first_solution(lines):
    return general_solution(lines, False)


def second_solution(lines):
    return general_solution(lines, True)


solution = Solution('../inputs/inputs_7.json', first_solution, second_solution)


def general_solution(lines, is_increasing):
    num_list = convert_string_list_to_ints(lines[0].split(','))
    minimum = min(num_list)
    previous = score_all(num_list, minimum, is_increasing)
    for num in range(minimum + 1, max(num_list)):
        score = score_all(num_list, num, is_increasing)
        if score > previous:
            return previous
        previous = score


def score_all(num_list, target, is_increasing):
    score = 0
    for num in num_list:
        score += score_one(num, target, is_increasing)
    return score


def score_one(value, target, is_increasing):
    distance = abs(target - value)
    if is_increasing:
        return sum(range(distance+1))
    return distance
