import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex)

const store = new Vuex.Store({
    state: {
        tags: [],
        searchPhrase: "",
        selectedNotes: [],
        focusedNote: null,
        movingElement: null,
        notes: {
            folders: [
                {
                    folders: [],
                    id: "11113",
                    name: "New Folder",
                    notes: [
                        {
                            content: "<h1>Note content 1</h1>",
                            id: "21131255",
                            links: ["21131266"],
                            name: "New 1",
                            tags: ["#tag"],
                        },
                        {
                            content: "<h1>Note content 2</h1>",
                            id: "21131266",
                            links: [],
                            name: "New 2",
                            tags: ["#tag"],
                        },
                        {
                            content: "<h1>Note content 3</h1>",
                            id: "21131288",
                            links: [],
                            name: "New 3",
                            tags: [],
                        },
                    ],
                }, {
                    folders: [],
                    id: "111133",
                    name: "New Folder 2",
                    notes: [
                        {
                            content: "<h1>Note content 1 2</h1>",
                            id: "211312553",
                            links: ["21131266"],
                            name: "New 1 2",
                            tags: ["#tag"],
                        },
                        {
                            content: "<h1>Note content 2 2</h1>",
                            id: "211312663",
                            links: [],
                            name: "New 2 2",
                            tags: ["#tag"],
                        },
                        {
                            content: "<h1>Note content 3 2</h1>",
                            id: "211312883",
                            links: [],
                            name: "New 3 2",
                            tags: [],
                        },
                    ],
                },
            ],
            id: "root",
            name: "root",
            notes: [
                {
                    content: "<h1>Note content</h1>",
                    id: "12353566",
                    links: ["21131255"],
                    name: "New Note",
                    tags: ["#tag"],
                },
            ],
        },
    },
    actions: {
    },
    mutations: {
        setTags(state, tags) {
            state.tags = tags;
        },
        setSearchPhrase(state, phrase) {
            state.searchPhrase = phrase;
        },
        addToSelectedNotes(state, note) {
            state.selectedNotes.push(note)
            state.focusedNote = note
        },
        setFocusedNote(state, note) {
            state.focusedNote = note;
        },
        setMovingElement(state, note) {
            state.movingElement = note;
        }
    },
    getters: {
        notes: (state) => {
            return state.notes;
        },
        selectedNotes: (state) => {
            return state.selectedNotes;
        },
        movingElement: (state) => {
            return state.movingElement;
        }
    }
});

export default store;