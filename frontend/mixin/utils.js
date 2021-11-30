export default {
  props: [
  ],
  data () {
    return {
      yomiDict: {
        あ: 'あいうえおアイウエオｱｲｳｴｵ',
        か: 'かきくけこカキクケコｶｷｸｹｺがぎぐげごガギグゲゴ',
        さ: 'さしすせそさしすせそｻｼｽｾｿさじずぜぞザジズゼゾ',
        た: 'たちつてとタチツテトﾀﾁﾂﾃﾄだぢづでどダヂヅデド',
        な: 'なにぬねのナニヌネノﾅﾆﾇﾈﾉ',
        は: 'はひふへほハヒフエホﾊﾋﾌﾍﾎばびぶべぼぱぴぷぺぽバビブベボパピプペポ',
        ま: 'まみむめもマミムメモﾏﾐﾑﾒﾓ',
        や: 'やゆよヤユヨﾔﾕﾖ',
        ら: 'らりるれろラリルレロﾗﾘﾙﾚﾛ',
        わ: 'わをんワヲンﾜｦﾝ'
      }
    }
  },
  methods: {
    validate () {
      if (this.$refs.form) {
        if (!this.$refs.form.validate()) {
          return false
        }
        return true
      }
      return true
    },
    fontColor (code) {
      if (code && code.substring) {
        const red = parseInt(code.substring(1, 3), 16)
        const green = parseInt(code.substring(3, 5), 16)
        const blue = parseInt(code.substring(5, 7), 16)
        const meido = (red * 299 + green * 587 + blue * 114) / 1000
        if (meido < 125) {
          return '#FFFFFF'
        } else {
          return '#000000'
        }
      } else {
        return 'white'
      }
    },
    formatDate (date) {
      if (!date) { return null }
      const parsedDate = new Date(date)
      const year = parsedDate.getFullYear()
      const month = ('0' + (parsedDate.getMonth() + 1)).slice(-2)
      const day = ('0' + parsedDate.getDate()).slice(-2)
      if (isNaN(parsedDate)) {
        return `${date}`
      } else {
        return `${year}/${month}/${day}`
      }
    },
    moveLink (url) {
      if (!url) {
        return
      }
      const reg = /^(https?|ftp):\/\/([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&%$-]+)*@)*((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3}|([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(\/($|[a-zA-Z0-9.,?'\\+&%$#=~_-]+))*$/
      if (!reg.test(url)) {
        return
      }

      window.open(url, '_blank', 'noreferrer')
    },
    getCnvYomi (c) {
      for (const key in this.yomiDict) {
        console.log(key)
        console.log(this.yomiDict[key])
        if (String(this.yomiDict[key]).search(c) !== -1) {
          return key
        }
      }
      return 'ー'
    }
  }
}
