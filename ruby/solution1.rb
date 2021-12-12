# frozen_string_literal: true

require_relative 'solution'

# Solution to December 1
class Solution1 < Solution

  private

  def first_solution(lines)
    lines.map(&:to_i).each_cons(2).select { |x| x[1] > x[0] }.count
  end

  def second_solution(lines)
    lines.map(&:to_i).each_cons(4).select { |x| x[3] > x[0] }.count
  end
end
