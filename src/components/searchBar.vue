<template>
  <div>
    <form>
        <input
          class="focus:outline-none w-full bg-nord-1 pl-2 py-1 block bg-nord-1 rounded text-white"
          type="text"
          placeholder="Search"
          @keydown="checkTagCompleted"
          @input="filterTagsFromSearch"
          v-model="fieldInput"
        />
    </form>
    <Tag-list :tags="tags" @remove-tag="(i) => tags.splice(i, 1)" />
  </div>
</template>

<script>
import TagList from "./tagList"

export default {
  name: "searchBar",
  components: {
    TagList
  },
  data: function() {
    return {
      fieldInput: "",
      search: "",
      tags: []
    };
  },
  methods: {
    addSearchCriteria: function() {
      const rule = /(#([^\s || #]{1,})?)/g;

      if (this.fieldInput.match(rule) !== null) {
        let tag = this.fieldInput.match(rule)[0]

        this.tags.push(tag.replace("#", ""))
      }
      
      this.fieldInput = this.fieldInput.replace(rule, "")
    },
    checkTagCompleted: function(e) {
      const tagSubmitKeys = ["Enter", " ", "#"]
      
      

      if (this.fieldInput.includes("#") && tagSubmitKeys.includes(e.key)) {
        e.preventDefault();
        this.addSearchCriteria();
      }
    },
    filterTagsFromSearch: function() {
      const rule = /(#([^\s || #]{1,})?)/g;

      this.$emit('toggle-pane')

      this.search = this.fieldInput.replace(rule, "")
    }
  },
};
</script>