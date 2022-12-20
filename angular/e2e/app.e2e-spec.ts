import { HotSpotWebTemplatePage } from './app.po';

describe('HotSpotWeb App', function() {
  let page: HotSpotWebTemplatePage;

  beforeEach(() => {
    page = new HotSpotWebTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
