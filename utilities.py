def print_answer(puzzle, result):
    print(f'The answer to {puzzle} is {result}')


def convert_to_action_tuples(a_list_of_actions):
    result = []
    string_list = convert_to_array(a_list_of_actions)
    for item in string_list:
        x_and_y = item.split()
        if x_and_y[0] == 'down':
            result.append((0, int(x_and_y[1])))
        if x_and_y[0] == 'up':
            result.append((0, -int(x_and_y[1])))
        if x_and_y[0] == 'forward':
            result.append((int(x_and_y[1]), 0))
    return result


def convert_to_array(a_line_separated_string):
    return a_line_separated_string.split("\n")
