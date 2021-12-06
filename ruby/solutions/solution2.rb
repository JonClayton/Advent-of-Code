# frozen_string_literal: true

require_relative 'solution'

# Solution to December 2
class Solution2 < Solution
  private

  def first_solution(lines)
    map_lines_to_actions(lines).reduce([0, 0]) { |a, v| [a[0] + v[0], a[1] + v[1]] }.reduce { |p, v| p * v }
  end

  def second_solution(lines)
    map_lines_to_actions(lines).reduce([0, 0, 0]) { |a, v| [a[0] + v[0], a[1] + a[2] * v[0], a[2] + v[1]]}.take(2).reduce { |p, v| p * v }
  end

  def map_lines_to_actions(lines)
    lines.map(&:split).map { |x| x[0] == 'forward' ? [x[1].to_i, 0] : [0, x[0] == 'up' ? -x[1].to_i : x[1].to_i] }
  end
end



