mergeInto(LibraryManager.library, {

  ShowNormalAdExtern: function() {
    console.log('ShowNormalAdExtern func work');
    ysdk.adv.showFullscreenAdv({
      callbacks: {
        onClose: function(wasShown) {
          myGameInstance.SendMessage('MyYandex', 'YaUnPauseMusic');
        },
        onError: function(error) {
          // some action on error
        }
      }
    })
  },


  ShowRewardExtern: function() {
    console.log('ShowRewardExtern func work');
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage('MyYandex', 'AddReward');
        },
        onClose: () => {
          myGameInstance.SendMessage('MyYandex', 'YaUnPauseMusic');
        },
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
      }
    })
  },

  CheckMobileExtern: function() {

    console.log('DEVICEEEEEEEEEEEEEEEEE!!!!!!!!!!!!!!!!!');
    
    var IsMobile = ysdk.deviceInfo.isMobile();

    if(IsMobile == true){
      myGameInstance.SendMessage('MyYandex', 'MobileTurnOn');
    }

  },
});