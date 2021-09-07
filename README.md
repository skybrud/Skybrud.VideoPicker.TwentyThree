# Skybrud.VideoPicker.TwentyThree

This is an extension to [Skybrud.VideoPicker](https://github.com/skybrud/Skybrud.VideoPicker) which adds a video provider for TwentyThree. 

## Config

In the ~/Config folder add `Skybrud.VideoPicker.config` with the following content:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<settings>
  <providers>
    <twentythree>
      <credentials
        id="fda08a63-57a8-4eb2-8131-d5efb7cd0302"
        name="Gubi"
        accessToken="[Add access token here]"
        accessTokenSecret="[Add access token secret here]"
        consumerKey="[Add consumer key here]"
        consumerSecret="[Add consumer secret here]" />
    </twentythree>
  </providers>
</settings>
```

Once the config is set up and the package is installed you can add the Skybrud.VideoPicker editor to a document type and toggle the TwentyThree provider enabled on the property. The property editor takes your video link and gets the rest of the info needed.
