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

    def get_note(self, note_name: str) -> Optional[Note]:
        for note in self.notes:
            if note.name == note_name:
                return note
        return None

    def get_notes_json(self) -> List[dict]:
        return [x.to_json() for x in self.notes]

    def create_new_note(self, note_data: dict):
        note_path = os.path.join(self.notes_directory, note_data.get('name') + ".html")
        with open(note_path, 'w') as f:
            f.write(note_data.get('contents'))

        self.notes.append(Note(note_path))

    def update_links(self, old_name: str, new_name: str):
        for note in self.notes:
            # Skip the note if it's the one being renamed
            # as if it's linking to itself this needs to be changed elsewhere
            # so that any contents changes don't overwrite it.
            if note.name == old_name:
                continue

            contents = note.get_note_contents()
            if f'[[{old_name}]]' in contents:
                contents = contents.replace(f'[[{old_name}]]', f'[[{new_name}]]')
                note.update({'contents': contents})

    def save_note(self, note_name: Optional[str], note_data: dict):
        self.load_notes()
        # If note name is none it needs to be created as there's no note being targeted for changes
        if note_name is not None:
            # Attempt to update a notes contents
            for note in self.notes:
                if note.name == note_name:
                    # The note is going to be renamed so we need to update all links
                    if note.name != note_data.get('name'):
                        self.update_links(note.name, note_data.get('name'))
                        note_data['contents'] = note_data.get('contents', '').replace(note.name, note_data.get('name'))

                    note.update(note_data)
                    return

        # Note doesn't already exist so it needs to be created
        self.create_new_note(note_data)
