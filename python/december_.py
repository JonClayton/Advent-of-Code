from python.solutions.classes import Solution
from python.solutions.utilities import convert_string_list_to_ints


def first_solution(lines):
    return general_solution(lines, 80)


def second_solution(lines):
    return general_solution(lines, 256)


solution = Solution('../inputs/inputs_7.json', first_solution, second_solution)

