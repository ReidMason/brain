function getTags(folder) {
    let tags = [];
    // Get tags from all notes in the folder
    folder.notes.forEach(note => {
        let matches = note.content.match(/(#[^\s || #]{1,})/g) || [];
        // Only add the tag if it hasn't been seen before
        if (matches) {
            matches.forEach(match => {
                if (!tags.includes(match)) {
                    tags.push(match)
                }
            });
        }
    })

    // Get tags from all folders in this folder
    folder.folders.forEach(f => {
        getTags(f).forEach(tag => {
            // Only add the tags if they aren't already in this tag list
            if (!tags.includes(tag)) {
                tags.push(tag);
            }
        })
    });
    return tags;
}

const Utils = {
    methods: {
        getAllTags() {
            return getTags(this.$store.state.notes);
        }
    }
}

export default Utils