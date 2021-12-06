# frozen_string_literal: true

Dir['./solutions/*.rb'].sort.each { |file| require file }

Solution1.new('../inputs/inputs_1.json').status_report
Solution2.new('../inputs/inputs_2.json').status_report
Solution3.new('../inputs/inputs_3.json').status_report

def solution_2_b(json_file_path)
  JSON.parse(File.read(json_file_path))['actual_input'].split("\n").map(&:split).map { |x| x[0] == 'forward' ? [x[1].to_i, 0] : [0, x[0] == 'up' ? -x[1].to_i : x[1].to_i] }.reduce([0, 0, 0]) { |a, v| [a[0] + v[0], a[1] + a[2] * v[0], a[2] + v[1]]}.take(2).reduce { |p, v| p * v }
end

puts(solution_2_b('../inputs/inputs_2.json'))