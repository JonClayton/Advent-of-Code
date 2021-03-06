from time import time_ns
import json


class Solution:
    def __init__(self, file_path, first_solution, second_solution):
        file_contents = open(file_path)
        data = json.load(file_contents)
        self.actual_lines = self._convert_to_array(data['actual_input'])
        self.day = data['day']
        self.first_solution = first_solution
        self.first_test_result = data['first_test_result']
        self.result = None
        self.second_solution = second_solution
        self.second_test_result = data['second_test_result']
        self.test_lines = self._convert_to_array(data['test_input'])

    def status_report(self):
        started_at = time_ns()
        if self.day is None or self.first_test_result is None:
            return print(f'Must set day and/or first_test_result in constructor for December {self.day}')
        self.result = self.first_solution(self.test_lines)
        if self.result != self.first_test_result:
            return self._report_failed_test(1, self.first_test_result)
        first_result = self.first_solution(self.actual_lines)
        self.result = self.second_solution(self.test_lines)
        if self.result != self.second_test_result:
            print(bcolors.WARNING + f'Solution for {self.day} part 1 is {first_result}')
            return self._report_failed_test(2, self.second_test_result)
        second_result = self.second_solution(self.actual_lines)
        elapsed_time = round((time_ns() - started_at) / 1000000000, 3)
        print(bcolors.OK + f'December {self.day} solved in {elapsed_time}s: first = {first_result} and second = {second_result}')

    def _report_failed_test(self, part, expected_result):
        print(bcolors.FAIL + f'Test for December {self.day} part {part}: failed with actual={self.result} and expected={expected_result}')

    @staticmethod
    def _convert_to_array(line_separated_string):
        return line_separated_string.split("\n")

class bcolors:
    HEADER = '\033[95m'
    OKBLUE = '\033[94m'
    OKCYAN = '\033[96m'
    OK = '\033[92m'
    WARNING = '\033[93m'
    FAIL = '\033[91m'
    ENDC = '\033[0m'
    BOLD = '\033[1m'
    UNDERLINE = '\033[4m'