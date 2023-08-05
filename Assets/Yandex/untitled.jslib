mergeInto(LibraryManager.library, {
    ShowAds: function () {
      ysdk.adv.showFullscreenAdv({
          callbacks: {
            onClose: function(wasShown) {
              MyGameInstance.SendMessage('Yandex', 'OnPlay');
            },
            onError: function(error) {
              MyGameInstance.SendMessage('Yandex', 'OnPlay');
            }
          }
      })
    },

    ShowRewardAds: function () {
            ysdk.adv.showRewardedVideo({
          callbacks: {
              onOpen: () => {
                console.log('Video ad open.');
              },
              onRewarded: () => {
                console.log('Rewarded!');
              },
              onClose: () => {
                console.log('Video ad closed.');
              },
              onError: (e) => {
                console.log('Error while open video ad:', e);
              }
          }
      })
    },

});
