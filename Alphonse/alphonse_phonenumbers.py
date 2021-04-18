import pathlib


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
def adapt_phone_numbers(numbers):
    result = []

    for number in numbers:
        number = number.translate({ord(i): None for i in ' _-.()[]\r\n'})
        if len(number) == 0:
            continue  # empty line
        if number.startswith("#"):
            continue  # commented line

        # add this number as is
        result.append(number)

        # try translate +33 xx... national phone number into local
        if number.startswith('+33'):
            result.append(number.replace("+33", "0", 1))

    return result


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class PhoneNumberFileExtractor:

    def __init__(self, file_path: str):
        self._file_path = file_path

    def get_numbers(self):
        if not pathlib.Path(self._file_path).is_file():
            return []

        with open(self._file_path, 'r') as file:
            lines = file.readlines()

        return adapt_phone_numbers(lines)
