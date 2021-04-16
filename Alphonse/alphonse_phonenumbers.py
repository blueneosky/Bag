import pathlib


# - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
class PhoneNumberFileExtractor:

    def __init__(self, file_path: str):
        self._file_path = file_path

    def get_numbers(self):
        result = []
        if not pathlib.Path(self._file_path).is_file():
            return result

        with open(self._file_path, 'r') as file:
            lines = file.readlines()

        for line in lines:
            line = line.translate({ord(i): None for i in ' _-.()[]\r\n'})
            if len(line) == 0:
                continue  # empty line
            if line.startswith("#"):
                continue  # commented line

            # add this number as is
            result.append(line)

            # try translate +33 xx... national phone number into local
            if line.startswith('+33'):
                result.append(line.replace("+33", "0", 1))

        return result


