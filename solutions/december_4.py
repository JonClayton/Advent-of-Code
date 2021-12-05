from classes import Solution
from inputs.inputs_4 import actual_input
from inputs.inputs_4 import test_input


def first_solution(lines):
    draw_list = convert_string_list_to_ints(lines[0].split(','))
    boards = get_boards(lines)
    for draw in draw_list:
        cover_number(boards, draw)
        for board in boards:
            if is_winner(board):
                return board_sum(board) * draw


def second_solution(lines):
    draw_list = convert_string_list_to_ints(lines[0].split(','))
    boards = get_boards(lines)
    for draw in draw_list:
        cover_number(boards, draw)
        for board in boards:
            if is_winner(board):
                if len(boards) == 1:
                    return board_sum(boards[0]) * draw
                boards.remove(board)


solution = Solution(4, test_input, actual_input, 4512, first_solution, 1924, second_solution)


def board_sum(board):
    result = 0
    for row in board:
        result += sum(row)
    return result


def convert_string_list_to_ints(string_list):
    result = []
    for item in string_list:
        result.append(int(item))
    return result


def cover_number(boards, draw):
    for board in boards:
        for row in board:
            for index in range(len(row)):
                if row[index] == draw:
                    row[index] = 0


def get_boards(lines):
    boards = []
    board = []
    for line in lines:
        if len(line) != 14:
            board = []
        else:
            board.append(convert_string_list_to_ints(line.split()))
            if len(board) == 5:
                boards.append(board)
    return boards


def is_column_winner(board, index):
    for row in board:
        if row[index] > 0:
            return False
    return True


def is_winner(board):
    for row in board:
        if sum(row) == 0:
            return True
    for index in range(len(board[0])):
        if is_column_winner(board, index):
            return True
    return False
