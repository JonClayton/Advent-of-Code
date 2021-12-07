from python.solutions.classes import Solution
from python.solutions.utilities import convert_string_list_to_ints


def first_solution(lines):
    return general_solution(lines, False)


def second_solution(lines):
    return general_solution(lines, True)


solution = Solution('../inputs/inputs_7.json', first_solution, second_solution)


def general_solution(lines, is_increasing):
    num_list = convert_string_list_to_ints(lines[0].split(','))
    uniques = sorted(list({num for num in num_list}))
    possibles = find_range_of_minima(uniques, num_list, is_increasing)
    winner = find_range_of_minima(possibles[0], num_list, is_increasing)
    return winner[1]


def find_range_of_minima(uniques, num_list, is_increasing):
    lowest = score_all(num_list, uniques[0], is_increasing)
    for idx in range(1, len(uniques)):
        score = score_all(num_list, uniques[idx], is_increasing)
        if score > lowest:
            return list(range(uniques[idx - min(2, idx)], uniques[idx] + 1)), lowest
        lowest = score


def score_all(num_list, target, is_increasing):
    score = 0
    for num in num_list:
        score += score_one(num, target, is_increasing)
    return score


def score_one(value, target, is_increasing):
    distance = abs(target - value)
    return (distance + 1) * distance / 2 if is_increasing else distance
