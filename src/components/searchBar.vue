<template>
  <div>
    <form @submit.prevent="$emit('listUpdated', fieldInput)">
        <input
          class="focus:outline-none w-full bg-nord-1 pl-2 py-1 block bg-nord-1 rounded text-white"
          type="text"
          placeholder="Search"
          @keyup="checkTagCompleted"
          @removeTag="console.log('testing')"
          v-model="fieldInput"
        />
    </form> 
    <Tag-list :tags="this.tags" />
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
      tags: []
    };
  },
  methods: {
    addSearchCriteria: function() {
      const rule = /(#[^\s || #]{1,})/g;

      if (this.fieldInput.match(rule) !== null) {
        let tag = this.fieldInput.match(rule)[0]
        let id = this.tags.length - 1

        this.tags.push({id: id, tag: tag.replace("#", "")})
      }
      
      this.fieldInput = this.fieldInput.replace(rule, "")
    },
    checkTagCompleted: function(e) {
      const tagSubmitKeys = ["Enter", " ", "#"]

      if (tagSubmitKeys.includes(e.key)) {
        this.addSearchCriteria();
      }
    }
  },
};
</script>