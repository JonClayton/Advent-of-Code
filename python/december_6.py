from python.classes import Solution
from python.utilities import convert_string_list_to_ints


def first_solution(lines):
    return general_solution(lines, 80)


def second_solution(lines):
    return general_solution(lines, 256)


solution = Solution('inputs/inputs_06.json', first_solution, second_solution)


def general_solution(lines, generations):
    initial_population = convert_string_list_to_ints(lines[0].split(','))
    population_by_age = {}
    for age in initial_population:
        population_by_age[age] = population_by_age.setdefault(age, 0) + 1
    for day in range(generations):
        new_dictionary = {}
        for age in range(8):
            new_dictionary[age] = population_by_age.setdefault(age + 1, 0)
        birthing_generation = population_by_age.setdefault(0, 0)
        new_dictionary[6] += birthing_generation
        new_dictionary[8] = birthing_generation
        population_by_age = new_dictionary
    return sum(population_by_age.values())
