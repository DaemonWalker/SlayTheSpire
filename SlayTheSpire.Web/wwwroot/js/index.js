Vue.component('new-select', {
    props: ['optionlist', 'vid', 'selectedvalue'],
    template: '<select v-bind:id="vid" class="form-control" @change="alertSync($event)">' +
        '<option v-for="op in optionlist" v-bind:value="op.id">{{op.text}}</option>' +
        '</select>',
    methods: {
        alertSync: function (e) {
            this.$emit('update:selectedvalue', e.target.value);
        }
    }
});
var app = new Vue({
    el: "#app",
    data: {
        saveData: '',
        saveBak: '',
        saveStr: '',
        saveFileName: '',
        tableInfo: { "": "" },
        cardColors: [],
        rarities: [],
        cardTypes: [],
        selectedCardColor: '0',
        selectedCardRarity: '0',
        selectedCardType: '0',
        queryCardName: '',
        queryCardDescription: '0',
        searchCards: [],
        selectedRelicRarity: '0',
        queryRelicName: '',
        queryRelicDes: '',
        queryFlavor: '',
        searchRelics: [],
        queryPotionName: '',
        queryPotionDes: '',
        searchPotions: []
    },
    created: function () {
        this.postData("/api/common/initCommon", '', null, res => {
            this.cardColors = res.data.cardColors;
            this.rarities = res.data.rarities;
            this.cardTypes = res.data.cardTypes;
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
                    this.saveData = JSON.parse(model.save);
                    this.tableInfo = model.tableInfo;
                    this.saveBak = JSON.parse(res.data.save);
                    $('#colEdit').collapse('show');
                });
        },
        exportSave: async function () {
            this.postData("/api/cheater/export", {
                "new": JSON.stringify(this.saveData),
                "bak": JSON.stringify(this.saveBak),
                "saveName": this.saveFileName
            }, "blob", res => {
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
                        'Content-Type': 'application/json;charset=utf-8'
                    }
                });
            }
            if (responseType === null) {
                return axios({
                    method: 'post',
                    url: url + apiUrl,
                    data: data,
                    headers: {
                        'Content-Type': 'application/json;charset=utf-8'
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
                    'Content-Type': 'application/json;charset=utf-8'
                }
            }).then(then).catch(function (error) {
                console.log(error);
            });
        },
        addCard: function (card) {
            console.log("card:  " + JSON.stringify(card));
            let info = new Object();
            info.name = card.name;
            info.description = card.description;
            if (this.tableInfo[card.id] === null) {
                this.tableInfo[card.id] = info;
            }

            let c = new Object();
            c.id = card.id;
            c.misc = 0;
            c.upgrades = 0;
            this.saveData.cards.push(c);
        },
        queryCards: function () {
            let obj = new Object();
            obj.name = this.queryCardName;
            obj.description = this.queryCardDescription;
            obj.color = this.selectedCardColor;
            obj.rarity = this.selectedCardRarity;
            obj.cardType = this.selectedCardType;
            this.postData('/api/card/query', obj, null, res => {
                this.searchCards = res.data;
            });
        },
        queryRelic: function () {
            let obj = new Object();
            obj.name = this.queryRelicName;
            obj.description = this.queryRelicDes;
            obj.rarity = parseInt(this.selectedRelicRarity);
            obj.flavor = this.queryFlavor;
            this.postData('/api/relic/query', obj, null, res => {
                this.searchRelics = res.data;
            });
        },
        addRelic: function (relic) {
            console.log("relic:  " + JSON.stringify(relic));
            let info = new Object();
            info.name = relic.name;
            info.description = relic.description;
            if (this.tableInfo[relic.id] === null ||
                this.tableInfo[relic.id] === undefined) {
                this.tableInfo[relic.id] = info;
            }

            let c = relic.id;
            this.saveData.relics.push(c);
        },
        queryPotions: function () {
            let obj = new Object();
            obj.name = this.queryRelicName;
            obj.description = this.queryRelicDes;
            obj.rarity = parseInt(this.selectedRelicRarity);
            obj.flavor = this.queryFlavor;
            this.postData('/api/potion/query', obj, null, res => {
                this.searchPotions = res.data;
            });
        },
        addPotion: function (potion) {
            console.log("potion:  " + JSON.stringify(potion));
            let info = new Object();
            info.name = potion.name;
            info.description = potion.description;
            if (this.tableInfo[potion.id] === null ||
                this.tableInfo[potion.id] === undefined) {
                this.tableInfo[potion.id] = info;
            }

            let c = potion.id;
            this.saveData.potions.push(c);
        },
        deleteRelic: function (relic) {
            let index = this.saveData.relics.indexOf(relic);
            this.saveData.relics.splice(index, 1);
        },
        deletePotion: function (potion) {
            let index = this.saveData.potions.indexOf(potion);
            this.saveData.potions.splice(index, 1);
        }
    },
    computed: {
        getName() {
            return function (id) {
                return this.tableInfo[id].name;
            };
        },
        getDescription(id) {
            return function (id) {
                return this.tableInfo[id].description;
            };
        }
    }
});