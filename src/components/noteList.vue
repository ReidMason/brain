<template>
  <div>
    <div class="h-12 text-2xl p-4 mb-4">Note List:</div>
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