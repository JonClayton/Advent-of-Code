from classes import Solution
from inputs.inputs_2 import actual_input
from inputs.inputs_2 import test_input


def first_solution(a_list_of_actions):
    steps = convert_to_action_tuples(a_list_of_actions)
    down = 0
    forward = 0
    for pair in steps:
        down += pair[1]
        forward += pair[0]
    return forward * down


def second_solution(a_list_of_actions):
    steps = convert_to_action_tuples(a_list_of_actions)
    aim = 0
    down = 0
    forward = 0
    for pair in steps:
        aim += pair[1]
        down += aim * pair[0]
        forward += pair[0]
    return forward * down


solution = Solution(2, test_input, actual_input, 150, first_solution, 900, second_solution)


def convert_to_action_tuples(a_list_of_actions):
    result = []
    for item in a_list_of_actions:
        x_and_y = item.split()
        if x_and_y[0] == 'down':
            result.append((0, int(x_and_y[1])))
        if x_and_y[0] == 'up':
            result.append((0, -int(x_and_y[1])))
        if x_and_y[0] == 'forward':
            result.append((int(x_and_y[1]), 0))
    return result
