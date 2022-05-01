import { HubConnectionBuilder, LogLevel, HttpTransportType} from '@aspnet/signalr'
export default {
  install(Vue) {
    const connection = new HubConnectionBuilder()
      .withUrl('http://localhost:39710/charthub',
        {
          skipNegotiation: true,
          transport: HttpTransportType.WebSockets
        })
      .configureLogging(LogLevel.Information)
      .build()

    let startedPromise = null
    function start() {
      startedPromise = connection.start().catch(err => {
        console.error('Failed to connect with hub', err)
        return new Promise((resolve, reject) =>
          setTimeout(() => start().then(resolve).catch(reject), 5000))
      })
      return startedPromise
    }
    connection.onclose(() => start())

    // use new Vue instance as an event bus
    const chartHub = new Vue()
    // every component will use this.$questionHub to access the event bus
    Vue.prototype.$chartHub = chartHub
    // Forward server side SignalR events through $questionHub, where components will listen to them
    connection.on('statistics-changed', (num, w) => {
      chartHub.$emit('statistics-changed', { num, w })
    })

    start()
  }
}
