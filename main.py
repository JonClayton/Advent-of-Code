import december_1
import december_2


def print_answer(puzzle, result):
    print(f'The answer to {puzzle} is {result}')


print_answer("December 1A", december_1.first(december_1.the_big_array))
print_answer("December 1B", december_1.second(december_1.the_big_array))
print_answer("December 2A", december_2.first(december_2.the_big_string))
print_answer("December 2B", december_2.second(december_2.the_big_string))

