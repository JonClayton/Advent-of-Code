# frozen_string_literal: true

require_relative 'solution'


# Solution to December 3
class Solution3 < Solution

  private

  def first_solution(lines)
  end

  def second_solution(lines)
  end


end


###

#
#
# def calculate_binary_value_of_string(binary_string):
#     result_array = []
#     for item in binary_string:
#         result_array.append(1 if item == '1' else 0)
#     return calculate_binary_value(result_array)
#
#
# def find_most_similar(candidates, is_inverted):
#     index = 0
#     while len(candidates) > 1:
#         target_array = invert_binary_array(most_common_array(candidates)) if is_inverted else most_common_array(
#             candidates)
#         meeting_condition = []
#         for candidate in candidates:
#             if int(candidate[index]) == target_array[index]:
#                 meeting_condition.append(candidate)
#         candidates = meeting_condition
#         index += 1
#     return candidates[0]
#
#
# def invert_binary_array(binary_array):
#     result = []
#     for value in binary_array:
#         result.append(0 if value == 1 else 1)
#     return result
#
#
# def most_common_array(observations):
#     result_array = []
#     majority = len(observations) / 2
#     for index in range(len(observations[0])):
#         count_1 = 0
#         for observation in observations:
#             if observation[index] == '1':
#                 count_1 += 1
#         result_array.append(1 if count_1 >= majority else 0)
#     return result_array