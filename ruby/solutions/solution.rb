require 'json'

# This reads puzzle information from json to test and apply solutions
class Solution
  attr_accessor :actual_lines, :created_at, :day, :first_test_result, :result, :second_test_result, :test_lines

  def initialize(file_path)
    data_hash = JSON.parse(File.read(file_path))
    @actual_lines = data_hash['actual_input'].split("\n")
    @created_at = Time.now
    @day = data_hash['day']
    @first_test_result = data_hash['first_test_result']
    @second_test_result = data_hash['second_test_result']
    @test_lines = data_hash['test_input'].split("\n")
  end

  # @return [nil]
  def status_report
    unless day && first_test_result
      puts("Must set day and/or first_test_result in constructor for December #{day}")
      return
    end
    @result = first_solution(test_lines)
    if result != first_test_result
      report_failed_test(1, first_test_result)
      return
    end
    first_result = first_solution(actual_lines)
    @result = second_solution(test_lines)
    if result != second_test_result
      puts("Solution for #{day} part 1 is #{first_result}")
      report_failed_test(2, second_test_result)
      return
    end
    second_result = second_solution(actual_lines)
    elapsed_time = ((Time.now.nsec - created_at.nsec) / 1_000_000_000.0).round(3)
    puts("December #{day} solved in #{elapsed_time}s: first = #{first_result} and second = #{second_result}")
  end

  private

  def first_solution(lines); end

  def report_failed_test(part, expected_result)
    puts("Test for December #{day} part #{part}: failed with actual=#{result} and expected=#{expected_result}")
  end

  def second_solution(lines); end
end
