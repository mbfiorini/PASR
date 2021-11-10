(function (define) {
    define(['jquery'], function ($) {
      return (function () {
        var pasr = pasr || {};

        pasr.maps = pasr.maps || {};

        // Prority Lists
        pasr.maps.priorityDesc = pasr.maps.priorityDesc || new Map();

        pasr.maps.priorityDesc.set('Max', 0);
        pasr.maps.priorityDesc.set('Normal',1);
        pasr.maps.priorityDesc.set('Min', 2);
        
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