(function (define) {
    define(['jquery'], function ($) {
      return (function () {
        var pasr = pasr || {};

        pasr.maps = pasr.maps || {};

        // Prority Lists
        pasr.maps.priorityDesc = pasr.maps.priorityDesc || new Map();

        pasr.maps.priorityDesc.set('max', 0);
        pasr.maps.priorityDesc.set('avarage',1);
        pasr.maps.priorityDesc.set('min', 2);
        
        pasr.maps.priorityList = pasr.maps.priorityList || ["Max", "Normal", "Min"];

        // console.log('Priotiry list:', priorityDesc.get('avarage'));

        return pasr;
      })();
    });
  }(typeof define === 'function' && define.amd
    ? define
    : function (deps, factory) {
      if (typeof module !== 'undefined' && module.exports) {
        module.exports = factory(require('jquery'));
      } else {
        window.pasr = factory(window.jQuery);
      }
    }));