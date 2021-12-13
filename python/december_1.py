from python.classes import Solution
from python.classes import bcolors


def first_solution(lines):
    return general_solution(lines, 1)


def second_solution(lines):
    return general_solution(lines, 3)


solution = Solution('inputs/inputs_01.json', first_solution, second_solution)


def general_solution(n, c):
    return len([x for x in range(c, len(n)) if n[x] > n[x - c]])
