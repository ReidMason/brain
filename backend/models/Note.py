import os
from datetime import datetime

import utils


class Note:
    def __init__(self, filepath: str):
        self.filepath = filepath
        self.name = None
        self.created = None
        self.modified = None
        self.get_note_data()

    def get_note_data(self):
        self.name = os.path.basename(self.filepath).rstrip('.html')
        self.created = datetime.fromtimestamp(os.path.getctime(self.filepath))
        self.modified = datetime.fromtimestamp(os.path.getmtime(self.filepath))

    def get_note_contents(self):
        """ Read and return the content of the note

        :return: The content of the note
        """
        with open(self.filepath, 'r') as f:
            return f.read()

    def update(self, note_data: dict):
        if note_data.get('name') != self.name:
            new_path = os.path.join(os.path.dirname(self.filepath), note_data.get('name') + '.html')
            os.rename(self.filepath, new_path)
            self.filepath = new_path

        with open(self.filepath, 'w') as f:
            f.write(note_data.get('contents'))

        self.get_note_data()

    def to_json(self, include_contents: bool = False):
        """ Gets information about the note in json format

        :param include_contents: Whether to include the contents of the note
        :return: Note information in json format
        """
        data = {
            'name'    : self.name,
            'created' : utils.js_format_datetime(self.created),
            'modified': utils.js_format_datetime(self.modified)
        }

        if include_contents:
            data['contents'] = self.get_note_contents()

        return data
