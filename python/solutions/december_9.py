from python.solutions.classes import Solution
from python.solutions.utilities import list_print
from python.solutions.utilities import convert_string_list_to_ints
from functools import reduce
from collections import Counter


def first_solution(lines):
    return 2


def second_solution(lines):
    return 1


class Solution9(Solution):
    def __init__(self, file_path):
        Solution.__init__(self, file_path, self.solution_a, self.solution_b)
        self.lists = []

    def solution_a(self, lines):
        (low_points, row, columns) = self.calculate_values(lines)
        return sum([self.get_value(point) + 1 for point in low_points])

    def solution_b(self, lines):
        (low_points, lists, row, columns) = calculate_values(lines)
        basin_sizes = sorted([self.get_basin_size(point) for point in low_points])
        basin_sizes.reverse()
        return basin_sizes[0] * basin_sizes[1] * basin_sizes[2]

    def calculate_values(self, lines):
        lists = [convert_string_list_to_ints(x) for x in lines]
        rows = len(lists)
        columns = len(lists[0])
        for item in lists:
            item.insert(columns, 9)
            item.insert(0, 9)
        lists.append([9 for x in range(columns + 2)])
        lists.insert(0, [9 for x in range(columns + 2)])
        result = []
        for row in range(1, rows + 1):
            for col in range(columns + 1):
                point = row, col
                if self.all_neighbors_higher(point):
                    result.append((row, col))
        self.lists = lists
        return result, rows, columns

    def all_neighbors_higher(self, point):
        value = self.get_value(point)
        neighbors = self.get_neighbors(point)
        return sum([0 if value < self.get_value(x) else 1 for x in neighbors]) == 0

    def get_basin_size(self, lower_point):
        value = self.get_value(lower_point)
        self.exclude_from_further_consideration(lower_point)
        neighbors = self.get_neighbors(lower_point)
        neighbors_in_same_basin = [x for x in neighbors if 9 > get_value(x, lists) >= value]
        return 1 if len(neighbors_in_same_basin) == 0 else 1 + sum([self.get_basin_size(n, lists) for n in neighbors_in_same_basin])

    def exclude_from_further_consideration(self, point):
        row, col = point
        self.lists[row][col] = -1

    @staticmethod
    def get_neighbors(point):
        row, col = point
        return [(row-1, col), (row, col-1), (row, col+1), (row + 1, col)]

    def get_value(self, point):
        row, col = point
        return self.lists[row][col]


solution = Solution9('../inputs/inputs_9.json')


