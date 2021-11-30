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
  computed: {
    permit () {
      return this.$store.state.permit
    },
    today () {
      return new Date(this.$store.state.taw.yyyy, this.$store.state.taw.mmdd.split('/')[0] - 1, this.$store.state.taw.mmdd.split('/')[1])
    },
    thisMonthStartDay () {
      return new Date(this.$store.state.taw.yyyy, this.$store.state.taw.mmdd.split('/')[0] - 1, 1)
    }
  },
  created () {
    if (this.$route.query.where) {
      const param = this.parseToken(this.$route.query.where)
      this.searchCondition = param
    }
    this.load()
  },
  methods: {
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
        return `${year}-${month}-${day}`
      }
    },
    showResultMessage (response) {
      if (response.data.resultMessage) {
        console.log(response)
        const rm = response.data.resultMessage
        this.showMessage(rm.title + ' ' + rm.text, rm.level)
      } else {
        this.showMessage('message:' + response.statusText, 'info')
      }
    },
    errorResultCheck (error) {
      if (error.response) {
        const res = error.response
        if (res.data.resultMessage) {
          const rm = res.data.resultMessage
          this.showMessage(rm.title + ' ' + rm.text, rm.level)
        } else {
          this.showMessage('message:' + res.statusText, 'error')
        }
      } else {
        console.log(error.response)
        let status = null
        if (error.response) {
          status = error.response.status
        }
        if (!status) {
          status = error.status
        }
        console.log(status)
        console.log('todo error')
        this.showMessage('message:' + error, 'error')
      }

      // alert(status)

      // if (status === 401) {
      //   console.log(error)
      //   alert(401)
      // } else if (status === 403) {
      //   console.log(error)
      //   alert(403)
      // } else if (status === 404) {
      //   console.log(error)
      //   alert(404)
      // } else if (status === 500) {
      //   console.log(error)
      //   alert(500)
      // } else if (status === 503) {
      //   console.log(error)
      //   alert(error)
      // } else if (error.response) {
      //   console.log(error)
      //   alert(error)
      // } else {
      // }
    },
    logout () {
      this.$auth.logout('local', {})
    },
    showMessage (message, variant) {
      this.$nuxt.$emit('snackbar', message, variant)
    },
    back (error) {
      // 引数がある場合のみステータスを判断
      if (error) {
        let status = null
        if (error.response) {
          status = error.response.status
        }
        if (!status) {
          status = error.status
        }
        // ステータスによっては、なにもしない
        if (status >= 500 && status <= 599) {
          window.location.replace('/')
        }
        if (status === 401) {
          this.logout()
          window.location.replace('/')
          // window.location = '/'
          return false
        }
      }
      window.history.length > 1 ? this.$router.go(-1) : this.$router.push('/')
      return true
    },
    clear () {
      this.load()
      return true
    },
    search () {
      this.load()
      return true
    },
    validate () {
      if (this.$refs.form) {
        if (!this.$refs.form.validate()) {
          return false
        }
        return true
      }
      return true
    },
    reset () {
      this.$refs.form.reset()
    },
    resetValidation () {
      this.$refs.form.resetValidation()
    },
    gotopage (url) {
      if (url) {
        this.$router.push(url)
      }
    },
    createToken (param) {
      const jwt = require('jsonwebtoken')
      // var token = jwt.sign({ foo: 'bar' }, 'shhhhh');
      if (param === undefined) {
        param = {}
      }
      param.timestamp = Math.floor(Date.now() / 5000)

      // var issuer = this.$store.state.signInUser === undefined ? "Anonymous" : this.$store.state.signInUser.userName
      // チケットの一部を暗号化の文字とする先頭５－２５ →今はaccountId
      const issuer = 'xxxxxxxxxxxxxxxxxxxxxxxxxxxxx'
      // const issuer = this.$auth.user.Id
      if (issuer) {
        const secret = issuer
        const token = jwt.sign(param, secret, { algorithm: 'HS256', issuer })
        // const token = encodeURIComponent(JSON.stringify(param))
        return token
      } else {
        return ''
      }
    },
    parseToken (token) {
      const jwt = require('jsonwebtoken')
      const issuer = 'xxxxxxxxxxxxxxxxxxxxxxxxxxxxx'
      // const param = jwt.decode(this.$route.query['id'], issuer)
      const param = jwt.decode(token, issuer)
      return param
    },
    moveLink (url) {
      if (!url) {
        return
      }
      console.log('xxxx333')
      const reg = /^(https?|ftp):\/\/([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&%$-]+)*@)*((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3}|([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(\/($|[a-zA-Z0-9.,?'\\+&%$#=~_-]+))*$/
      if (!reg.test(url)) {
        console.log('xxxx')
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
