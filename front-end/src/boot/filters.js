export default ({ Vue }) => {

  Vue.filter('formatMoney', function (value) {
    if (!value || value === null || value === '') { return 'R$ 0,00' }
    if (typeof 'string') value = parseFloat(value)

    function numberToReal(numero) {
      numero = numero.toFixed(2).split('.')
      numero[0] = 'R$ ' + numero[0].split(/(?=(?:...)*$)/).join('.')
      return numero.join(',')
    }

    let x = numberToReal(value)
    return x
  })

  Vue.filter('formatPerc', function (value) {
    if (!value) { return '0,00 %' }
    function numberToReal(numero) {
      numero = numero.toFixed(2).split('.')
      numero[0] = numero[0].split(/(?=(?:...)*$)/).join('.')
      return numero.join(',') + ' %'
    }
    let x = numberToReal(value)
    return x
  })

  Vue.filter('formatDate', function (value) {
    if (value === null) { return null }
    // Primeiro deve ser feito a correção do timezone da data para evitar inconsistencias
    let date = new Date(value).setMinutes(new Date(value).getTimezoneOffset())
    return new Date(date).toLocaleString(
      'pt-BR',
      { year: 'numeric', month: '2-digit', day: '2-digit' }
    )
  })

  Vue.filter('capitalize', function (value) {
    if (!value) return ''
    value = value.toString().toLowerCase()
    return value.charAt(0).toUpperCase() + value.slice(1)
  })
}
