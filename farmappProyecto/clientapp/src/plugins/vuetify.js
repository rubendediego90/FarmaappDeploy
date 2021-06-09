import Vue from 'vue';
import Vuetify from 'vuetify/lib'

Vue.use(Vuetify);

import es from 'vuetify/es5/locale/es'

Vue.component('my-component', {
  methods: {
    changeLocale () {
      this.$vuetify.lang.current = 'es'
    },
  },
})

export default new Vuetify({
  lang: {
    locales: { es},
    current: 'es',
  },
  theme: {
      options: {
        customProperties: true,
      },
    themes: {
      light: {
        primary: '#800080',
        secondary: '#424242',
        accent: '#82B1FF',
        error: '#FF5252',
        info: '#2196F3',
        success: '#4CAF50',
        warning: '#FFC107'
      },
    },
  },
  icons: {
    iconfont: 'md',
  },
});



