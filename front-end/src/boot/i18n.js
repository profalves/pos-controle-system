import Vue from 'vue'
import VueI18n from 'vue-i18n'
import messages from 'src/i18n'
import { Quasar } from 'quasar'

const currentLanguage = Quasar.lang.getLocale()

Vue.use(VueI18n)

const i18n = new VueI18n({
  locale: currentLanguage,
  fallbackLocale: 'pt-br',
  messages
})

export default ({ app }) => {
  app.i18n = i18n
}

export { i18n }
