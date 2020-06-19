import os
from typing import List, Optional

import utils
from models.Note import Note


class Notes:
    def __init__(self, notes_directory: str):
        self.notes_directory = notes_directory
        self.notes: List[Note] = []
        self.load_notes()

    def load_notes(self):
        utils.ensure_path_exists(self.notes_directory)
        notes = [x for x in os.listdir(self.notes_directory) if x.endswith('.html')]
        self.notes = [Note(os.path.join(self.notes_directory, x)) for x in notes]

    def get_note(self, note_id: str) -> Optional[Note]:
        for note in self.notes:
            if note.id == note_id:
                return note
        return None

    def get_notes_json(self) -> List[dict]:
        return [x.to_json() for x in self.notes]
