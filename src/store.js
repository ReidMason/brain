import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex)

const store = new Vuex.Store({
    state: {
        tags: [],
        searchPhrase: "",
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
    actions: {},
    mutations: {
        setTags(state, tags) {
            state.tags = tags;
        },
        setSearchPhrase(state, phrase) {
            state.searchPhrase = phrase;
        }
    }
});

export default store;