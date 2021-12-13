from python.classes import Solution


def first_solution(lines):
    result_array = most_common_array(lines)
    gamma = calculate_binary_value(result_array)
    epsilon = calculate_binary_value((invert_binary_array(result_array)))
    return gamma * epsilon


def second_solution(lines):
    oxygen_generator_rating = find_most_similar(lines, 1)
    co2_scrubber_rating = find_most_similar(lines, 0)
    return calculate_binary_value_of_string(oxygen_generator_rating) * calculate_binary_value_of_string(co2_scrubber_rating)


solution = Solution('inputs/inputs_03.json', first_solution, second_solution)


def calculate_binary_value(binary_array):
    result = 0
    for index in range(len(binary_array)):
        result += binary_array[index] * 2 ** (len(binary_array) - index - 1)
    return result


def calculate_binary_value_of_string(binary_string):
    result_array = []
    for item in binary_string:
        result_array.append(1 if item == '1' else 0)
    return calculate_binary_value(result_array)


def find_most_similar(candidates, is_inverted):
    index = 0
    while len(candidates) > 1:
        target_array = invert_binary_array(most_common_array(candidates)) if is_inverted else most_common_array(
            candidates)
        meeting_condition = []
        for candidate in candidates:
            if int(candidate[index]) == target_array[index]:
                meeting_condition.append(candidate)
        candidates = meeting_condition
        index += 1
    return candidates[0]


def invert_binary_array(binary_array):
    result = []
    for value in binary_array:
        result.append(0 if value == 1 else 1)
    return result


def most_common_array(observations):
    result_array = []
    majority = len(observations) / 2
    for index in range(len(observations[0])):
        count_1 = 0
        for observation in observations:
            if observation[index] == '1':
                count_1 += 1
        result_array.append(1 if count_1 >= majority else 0)
    return result_array
