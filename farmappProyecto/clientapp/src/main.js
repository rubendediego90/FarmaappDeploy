import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import axios from 'axios'
import vuetify from './plugins/vuetify';
import VueMdijs from 'vue-mdijs'
import { mdiMagnify } from '@mdi/js'

VueMdijs.add({ mdiMagnify })
Vue.use(VueMdijs)


Vue.config.productionTip = false
axios.defaults.baseURL='http://localhost:2085'

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
