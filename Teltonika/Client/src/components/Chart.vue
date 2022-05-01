<template>
  <div>
    <div id="app" class="vue-tempalte" align="center">
      <reactive-bar-chart :chart-data="chartData" style="width: 500px; height:500px"></reactive-bar-chart>
    </div>
    <div id="app2">
      <select name="filter" @change="onChange($event)" v-model="key">
        <option value="1">Age bracket</option>
        <option value="2">Gender</option>
        <option value="3">Municipality</option>
        <option value="4">Confirmation date</option>
      </select>
    </div>
  </div>
</template>

<script>
  import ReactiveBarChart from "./ChartScript.js";
  import api from '@/ChartAPIService';

  export default {
    name: "App",
    components: {
      ReactiveBarChart
    },
    data() {
      return {
        chartData: null,
        key: "1",
        chartLabel: "Age bracket"
      };
    },
    created() {
      // Listen to score changes coming from SignalR events
      this.$chartHub.$on('statistics-changed', this.onStatisticsChanged)
    },
    beforeDestroy() {
      // Make sure to cleanup SignalR event handlers when removing the component
      this.$chartHub.$off('statistics-changed', this.onStatisticsChanged)
    },
    methods: {
      onStatisticsChanged() {
        this.onChange(null)
      },
      onChange(event) {
        //If method was called as button event
        if (event != null) {
          this.key = event.target.value
          this.filterChanged(event.target.value)
        }
        //If method was called manually to get latest data
        else {
          this.filterChanged(this.key)
        }
      },
      filterChanged(val){
        switch (val) {
          case "1":
            this.getChartData('AgeBracket');
            this.chartLabel = "Age bracket"
            break;
          case "2":
            this.getChartData('Gender');
            this.chartLabel = "Gender"
            break;
          case "3":
            this.getChartData('Municipality');
            this.chartLabel = "Municipality"
            break;
          case "4":
            this.getChartData('ConfirmationDate');
            this.chartLabel = "ConfirmationDate"
            break;
          default:
            break
        }
      },

      async getChartData(group) {
        try {
          var records = await api.getChartData(group)
        }
        finally {
          var l = records.map((r) => [r.Group]);
          var s = records.map((r) => [r.Count]);

          this.chartData = {
            labels: l,
            datasets: [
              {
                label: this.chartLabel,
                backgroundColor: "#f87979",
                data: s,
              }
            ],
          };
        }
      },
    },
    mounted() {
      this.getChartData('AgeBracket');
    }
  };
</script>

