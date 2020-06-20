import Vue from 'vue'
import Vuex from 'vuex'
import App from './App.vue'

Vue.config.productionTip = false
Vue.use(Vuex)

const store = new Vuex.Store({
  state: {
    tags: [],
    searchPhrase: '',
    notes: [
      { id: '0', title: 'First', content: 'Testing' },
      { id: '1', title: 'Second', content: 'Testing again' },
      { id: '2', title: 'Third', content: 'Testing a third time' }
    ]
  },
  actions: {
    
  },
  mutations: {
    setTags (state, tags) {
      state.tags = tags
    },
    setSearchPhrase (state, phrase) {
      state.searchPhrase = phrase
    }
  },
  getters: {

  }
})

new Vue({
  store,
  render: h => h(App),
}).$mount('#app')