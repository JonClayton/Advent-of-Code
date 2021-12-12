from python.classes import Solution
from functools import reduce
from collections import Counter


def first_solution(lines):
    return len([x for x in reduce(lambda a, x: a + x, [x[1].split() for x in [x.split(' | ') for x in lines]]) if len(x) in [2, 3, 4, 7]])


def second_solution(lines):
    # return reduce(lambda x: BustedDisplay(x).value(), lines)
    return 0

solution = Solution('inputs/inputs_8.json', first_solution, second_solution)

#
# def add_value_for_line(previous_total, line):
#     samples = line.split(' | ')
#     result_indexes = find_matches(samples[0], samples[1])
#     value_array = deduce_values(samples[0])
#     return previous_total + calculate_value(result_indexes, value_array)
#
#
# def deduce_values(ten_samples)
#
# def find_matches(full_set, to_find):
#     sorted_full_set = list(map(lambda x: sorted(x), full_set))
#     return map(lambda x: sorted_full_set.index(sorted(x)), to_find)
#
# class BustedDisplay:
#     def __init__(self, line):
#         split_line = line.split(" | ")
#         self.samples = split_line[0].split()
#         self.report = split_line[1].split()
#         self.sample_values = {}
#         self.wire_connections = {}
#         self.correct_connections = {
#             'CF': 1,
#             'ACDEG': 2,
#             'ACDFG': 3,
#             'BCDF': 4,
#             'ABDFG': 5,
#             'ABDEFG': 6,
#             'ACF': 7,
#             'ABCDEFG': 8,
#             'ABCDFG': 9,
#             'ABCEFG': 0,
#         }
#
#     def value(self):
#         self._deduce_values_for_samples()
#         return self.calculate_value()
#
#     def _deduce_values_for_samples(self):
#         one = None
#         four = None
#         for sample in self.samples:
#             if len(sample) == 2:
#                 one = sample
#             if len(sample) == 4:
#                 four = sample
#         tallies = Counter([item for sub in self.samples for item in sub])
#         a_or_c = filter(lambda k, v: v == 8, tallies.items())
#         d_or_g = filter(lambda k, v: v == 7, tallies.items())
#
#         self.wire_connections = {
#             list(filter(lambda k, v: v not in one, a_or_c))[0][0] : "A",
#             list(filter(lambda k, v: v == 6, tallies.items()))[0][0] : 'B',
#             list(filter(lambda k, v: v in one, a_or_c))[0][0] :'C',
#             list(filter(lambda k, v: v in four, d_or_g))[0][0] :'D',
#             list(filter(lambda k, v: v == 4, tallies.items()))[0][0] :'E',
#             list(filter(lambda k, v: v == 9, tallies.items()))[0][0] :'F',
#             list(filter(lambda k, v: v in four, d_or_g))[0][0] :'G'
#         }
#         for sample in self.samples:
#             self.sample_values[sorted(sample)] = self.correct_connections.get(sorted(reduce(lambda l: self.wire_connections.get(l), sample)))
#
#
#
#
#
#
#
#
#
#
#
#
#
# #
# # dict = {
# #     1: 'CF',
# #     7: 'ACF',
# #     4: 'BCDF',
# #     5: 'ABDFG',
# #     2: 'ACDEG',
# #     3: 'ACDFG',
# #     6: 'ABDEFG',
# #     9: 'ABCDFG'
# #     0: 'ABCEFG',
# #     8: 'ABCDEFG',
# # }
# #
# #
# # counts = {
# #     'F': 9,
# #     'A': 8, # not in 1
# #     'C': 8, # in 1
# #     'D': 7, # in 4
# #     'G': 7, # not in 4
# #     'B': 6,
# #     'E': 4,
# }