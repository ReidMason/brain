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

    def to_json(self):
        return {
            'name'    : self.name,
            'id'      : self.id,
            'created' : utils.js_format_datetime(self.created),
            'modified': utils.js_format_datetime(self.created)
        }
