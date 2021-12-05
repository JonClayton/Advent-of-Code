from python.solutions.classes import Solution


def first_solution(lines):
    return count_that_exceed_previous_observation(lines)


def second_solution(lines):
    return count_that_exceed_previous_observation(lines, 3)


solution = Solution('../inputs/inputs_1.json', first_solution, second_solution)


def count_that_exceed_previous_observation(num_list, count_back=1):
    answer = 0
    for current in range(count_back, len(num_list)):
        if num_list[current] > num_list[current - count_back]:
            answer += 1
    return answer
