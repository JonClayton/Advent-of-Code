class Solution:
    def __init__(self, day, test_input, actual_input, first_test_result, first_solution, second_test_result,
                 second_solution):
        self.actual_lines = self._convert_to_array(actual_input)
        self.day = day
        self.first_solution = first_solution
        self.first_test_result = first_test_result
        self.result = None
        self.second_solution = second_solution
        self.second_test_result = second_test_result
        self.test_lines = self._convert_to_array(test_input)

    def status_report(self):
        if self.day is None or self.first_test_result is None:
            return print(f'Must set day and/or first_test_result in constructor for December {self.day}')
        self.result = self.first_solution(self.test_lines)
        if self.result != self.first_test_result:
            return self._report_failed_test(1, self.first_test_result)
        first_result = self.first_solution(self.actual_lines)
        self.result = self.second_solution(self.test_lines)
        if self.result != self.second_test_result:
            print(f'Solution for {self.day} part 1 is {first_result}')
            return self._report_failed_test(2, self.second_test_result)
        second_result = self.second_solution(self.actual_lines)
        print(f'Solutions for December {self.day} are part 1: {first_result} and part 2: {second_result}')

    def _report_failed_test(self, part, expected_result):
        print(f'Test for December {self.day} part {part}: failed with actual={self.result} and expected={expected_result}')

    @staticmethod
    def _convert_to_array(line_separated_string):
        return line_separated_string.split("\n")
