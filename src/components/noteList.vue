<template>
  <div>
    <h2>Note List:</h2>
    <noteItem v-for="note in notes" :key="note.id" :details="note" />
  </div>
</template>

<script>
import noteItem from "./noteItem.vue";
import { mapState } from 'vuex'

export default {
  components: {
    noteItem
  },
  data: function() {
    return {};
  },
  computed: {
      ...mapState(['test', 'notes'])
  }
};
</script>