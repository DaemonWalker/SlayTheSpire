Vue.component('new-select', {
    props: ['optionlist', 'vid', 'selectedvalue'],
    template: '<select v-bind:id="vid" class="form-control" v-model="compSelectedValue">' +
        '<option v-for="op in optionlist" v-bind:value="op.id">{{op.text}}</option>' +
        '</select>',
    data: function () {
        return {
            compSelectedValue: this.selectedvalue
        };
    }
});
var app = new Vue({
    el: "#app",
    data: {
        saveData: '',
        saveBak: '',
        saveStr: '',
        saveFileName: '',
        cardInfo: { "": "" },
        cardColors: [],
        rarities: [],
        cardTypes: [],
        selectedCardColor: ''
    },
    created: function () {
        this.postData("/api/common/initCommon", '', null, res => {
            this.cardColors = res.data.cardColors;
            this.rarities = res.data.rarities;
            this.cardTypes = res.data.cardTypes;
            console.log('123' + res.data.cardColors);
        });
    },
    methods: {
        deleteCard: function (card) {
            let index = this.saveData.cards.indexOf(card);
            this.saveData.cards.splice(index, 1);
        },
        convertSave: async function () {
            this.postData("/api/cheater/upload", '"' + this.saveStr + '"', null,
                res => {
                    let model = res.data;
                    console.log("model:" + model.save);
                    this.saveData = JSON.parse(model.save);
                    this.cardInfo = model.cardInfo;
                    this.saveBak = JSON.parse(res.data.save);
                    console.log(this.cardInfo);
                });
        },
        exportSave: async function () {
            this.postData("/api/cheater/export", {
                "new": JSON.stringify(this.saveData),
                "bak": JSON.stringify(this.saveBak),
                "saveName": this.saveFileName
            }, "blob", res => {
                //let blob = new Blob([res.data], { type: "application/x-zip-compressed" });
                //let objectUrl = URL.createObjectURL(blob);
                //let link = document.createElement('a');
                //link.style.display = 'none';
                //link.href = objectUrl;
                //link.setAttribute('download', 'excel.xlsx');

                //document.body.appendChild(link);
                //link.click();

                //window.location.href = objectUrl;
                let headers = res.headers;
                let contentType = headers['content-type'];
                const blob = new Blob([res.data], { type: contentType });
                const contentDisposition = res.headers['content-disposition'];
                let fileName = 'unknown';
                if (contentDisposition) {
                    fileName = window.decodeURI(res.headers['content-disposition'].split(';')[1].split('=')[1]);
                }
                this.downFile(blob, fileName);
            });
        },
        downFile: function (blob, fileName) {
            // 非IE下载
            if ('download' in document.createElement('a')) {
                let link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob); // 创建下载的链接
                link.download = fileName; // 下载后文件名
                link.style.display = 'none';
                document.body.appendChild(link);
                link.click(); // 点击下载
                window.URL.revokeObjectURL(link.href); // 释放掉blob对象
                document.body.removeChild(link); // 下载完成移除元素
            } else {
                // IE10+下载
                window.navigator.msSaveBlob(blob, fileName);
            }
        },
        postData: async function (apiUrl, data, responseType, then) {
            var url = await getSaveServiceUrl();
            if (then === null) {
                return axios({
                    method: 'post',
                    url: url + apiUrl,
                    data: data,
                    headers: {
                        'Content-Type': 'application/json'
                    }
                });
            }
            if (responseType === null) {
                return axios({
                    method: 'post',
                    url: url + apiUrl,
                    data: data,
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(then).catch(function (error) {
                    console.log(error);
                });
            }
            axios({
                method: 'post',
                url: url + apiUrl,
                data: data,
                responseType: responseType,
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(then).catch(function (error) {
                console.log(error);
            });
        },
        test: function () {
            alert(this.selectedCardColor);
        }
    },
    computed: {
        getCardName() {
            return function (cardName) {
                return this.cardInfo[cardName].name;
            };
        },
        getCardDescription() {
            return function (cardName) {
                return this.cardInfo[cardName].description;
            };
        }
    }
});