import os
from datetime import datetime

import utils


class Note:
    def __init__(self, filepath: str):
        self.filepath = filepath
        self.id = str(os.path.getctime(self.filepath))
        self.name = os.path.basename(filepath).rstrip('.html')
        self.created = datetime.fromtimestamp(float(self.id))
        self.modified = datetime.fromtimestamp(os.path.getmtime(self.filepath))

    def get_note_contents(self):
        """ Read and return the content of the note

        :return: The content of the note
        """
        with open(self.filepath, 'r') as f:
            return f.read()

    def to_json(self, include_contents: bool = False):
        """ Gets information about the note in json format

        :param include_contents: Whether to include the contents of the note
        :return: Note information in json format
        """
        data = {
            'name'    : self.name,
            'id'      : self.id,
            'created' : utils.js_format_datetime(self.created),
            'modified': utils.js_format_datetime(self.created)
        }

        if include_contents:
            data['contents'] = self.get_note_contents()

        return data
