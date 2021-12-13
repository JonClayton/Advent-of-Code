from python.classes import Solution
from python.utilities import convert_string_list_to_ints
import numpy as np
import math
from functools import reduce


def first_solution(lines):
    return general_solution(lines, False)


def second_solution(lines):
    return general_solution(lines, True)


solution = Solution('inputs/inputs_07.json', first_solution, second_solution)


def general_solution(lines, is_increasing):
    num_list = np.array(convert_string_list_to_ints(lines[0].split(',')))
    location = np.mean(num_list).astype(float) if is_increasing else np.median(num_list)
    targets = [math.floor(location), math.ceil(location)] if math.floor(location) != location else [location]
    return min([reduce(lambda a, v: a + ((abs(v-t) + 1) * abs(v-t) / 2 if is_increasing else abs(v-t)), num_list, 0) for t in targets])
