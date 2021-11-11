using System;
using System.Collections.Generic;
using System.Text;
using ZeroFormatOSM.TagConvertion;

namespace ZeroFormatOSM {
	public enum TagType {
		/// <summary>
		/// Unknown tag or tag could not be deserialized or no value set (default).
		/// </summary>
		ZeroUnavailable = 0,

		/// <summary>
		/// Elevation (height above sea level) of a point, in metres.
		/// </summary>
		[OSMTagType("ele")]
		Elevation,

		[OSMTagType("is_in")]
		IsIn,

		[OSMTagType("is_in:country_code")]
		IsInCountryCode,

		[OSMTagType("is_in:iso_3166_2")]
		IsInIso3166_2,

		[OSMTagType("name")]
		Name,

		[OSMTagType("name:ar")]
		NameAr,

		[OSMTagType("name:fa")]
		NameFa,

		[OSMTagType("name:hu")]
		NameHu,

		[OSMTagType("name:ja")]
		NameJa,

		[OSMTagType("name:la")]
		NameLa,

		[OSMTagType("name:lt")]
		NameLt,

		[OSMTagType("name:nds")]
		NameNds,

		[OSMTagType("name:ru")]
		NameRu,

		[OSMTagType("name:sr")]
		NameSr,

		[OSMTagType("openGeoDB:community_identification_number")]
		OpengeodbCommunityIdentificationNumber,

		[OSMTagType("openGeoDB:license_plate_code")]
		OpengeodbLicensePlateCode,

		[OSMTagType("openGeoDB:loc_id")]
		OpengeodbLocId,

		[OSMTagType("openGeoDB:postal_codes")]
		OpengeodbPostalCodes,

		[OSMTagType("openGeoDB:telephone_area_code")]
		OpengeodbTelephoneAreaCode,

		[OSMTagType("place")]
		Place,

		[OSMTagType("population")]
		Population,

		[OSMTagType("population:date")]
		PopulationDate,

		[OSMTagType("ref:LOCODE")]
		RefLOCODE,

		[OSMTagType("wikidata")]
		Wikidata,

		[OSMTagType("wikipedia")]
		Wikipedia,

		[OSMTagType("highway")]
		Highway,

		[OSMTagType("created_by")]
		CreatedBy,

		[OSMTagType("button_operated")]
		ButtonOperated,

		[OSMTagType("crossing")]
		Crossing,

		[OSMTagType("note")]
		Note,

		[OSMTagType("railway")]
		Railway,

		[OSMTagType("ref")]
		Ref,

		[OSMTagType("railway:milestone:catenary_mast")]
		RailwayMilestoneCatenaryMast,

		[OSMTagType("railway:position")]
		RailwayPosition,

		[OSMTagType("amenity")]
		Amenity,

		[OSMTagType("fuel:lpg")]
		FuelLpg,

		[OSMTagType("wheelchair")]
		Wheelchair,

		[OSMTagType("crossing:barrier")]
		CrossingBarrier,

		[OSMTagType("railway:position:exact")]
		RailwayPositionExact,

		[OSMTagType("bus")]
		Bus,

		[OSMTagType("operator")]
		Operator,

		[OSMTagType("public_transport")]
		PublicTransport,

		[OSMTagType("shelter")]
		Shelter,

		[OSMTagType("tactile_paving")]
		TactilePaving,

		[OSMTagType("opening_hours")]
		OpeningHours,

		[OSMTagType("shop")]
		Shop,

		[OSMTagType("recycling:clothes")]
		RecyclingClothes,

		[OSMTagType("recycling:glass_bottles")]
		RecyclingGlassBottles,

		[OSMTagType("recycling:paper")]
		RecyclingPaper,

		[OSMTagType("recycling_type")]
		RecyclingType,

		[OSMTagType("barrier")]
		Barrier,

		[OSMTagType("recycling:glass")]
		RecyclingGlass,

		[OSMTagType("traffic_signals:direction")]
		TrafficSignalsDirection,

		[OSMTagType("waterway")]
		Waterway,

		[OSMTagType("addr:city")]
		AddrCity,

		[OSMTagType("addr:country")]
		AddrCountry,

		[OSMTagType("addr:housenumber")]
		AddrHousenumber,

		[OSMTagType("addr:postcode")]
		AddrPostcode,

		[OSMTagType("addr:street")]
		AddrStreet,

		[OSMTagType("email")]
		Email,

		[OSMTagType("outdoor_seating")]
		OutdoorSeating,

		[OSMTagType("phone")]
		Phone,

		[OSMTagType("start_date")]
		StartDate,

		[OSMTagType("toilets:wheelchair")]
		ToiletsWheelchair,

		[OSMTagType("website")]
		Website,

		[OSMTagType("crossing:light")]
		CrossingLight,

		[OSMTagType("crossing:saltire")]
		CrossingSaltire,

		[OSMTagType("crossing:supervision")]
		CrossingSupervision,

		[OSMTagType("bench")]
		Bench,

		[OSMTagType("local_ref")]
		LocalRef,

		[OSMTagType("train")]
		Train,

		[OSMTagType("crossing_ref")]
		CrossingRef,

		[OSMTagType("fee")]
		Fee,

		[OSMTagType("bicycle")]
		Bicycle,

		[OSMTagType("atm")]
		Atm,

		[OSMTagType("check_date:opening_hours")]
		CheckDateOpeningHours,

		[OSMTagType("brand")]
		Brand,

		[OSMTagType("brand:wikidata")]
		BrandWikidata,

		[OSMTagType("brand:wikipedia")]
		BrandWikipedia,

		[OSMTagType("cuisine")]
		Cuisine,

		[OSMTagType("delivery")]
		Delivery,

		[OSMTagType("drink:club-mate")]
		DrinkClubMate,

		[OSMTagType("internet_access:fee")]
		InternetAccessFee,

		[OSMTagType("internet_access:ssid")]
		InternetAccessSsid,

		[OSMTagType("opening_hours:covid19")]
		OpeningHoursCovid19,

		[OSMTagType("smoking")]
		Smoking,

		[OSMTagType("takeaway")]
		Takeaway,

		[OSMTagType("internet_access")]
		InternetAccess,

		[OSMTagType("surveillance")]
		Surveillance,

		[OSMTagType("historic")]
		Historic,

		[OSMTagType("memorial")]
		Memorial,

		[OSMTagType("service:bicycle:rental")]
		ServiceBicycleRental,

		[OSMTagType("service:bicycle:repair")]
		ServiceBicycleRepair,

		[OSMTagType("service:bicycle:retail")]
		ServiceBicycleRetail,

		[OSMTagType("diet:vegan")]
		DietVegan,

		[OSMTagType("diet:vegetarian")]
		DietVegetarian,

		[OSMTagType("wheelchair:description")]
		WheelchairDescription,

		[OSMTagType("height")]
		Height,

		[OSMTagType("image")]
		Image,

		[OSMTagType("dispensing")]
		Dispensing,

		[OSMTagType("healthcare")]
		Healthcare,

		[OSMTagType("layer")]
		Layer,

		[OSMTagType("leisure")]
		Leisure,

		[OSMTagType("addr:housename")]
		AddrHousename,

		[OSMTagType("crossing:island")]
		CrossingIsland,

		[OSMTagType("railway:signal:direction")]
		RailwaySignalDirection,

		[OSMTagType("railway:signal:main")]
		RailwaySignalMain,

		[OSMTagType("railway:signal:main:form")]
		RailwaySignalMainForm,

		[OSMTagType("railway:signal:main:height")]
		RailwaySignalMainHeight,

		[OSMTagType("railway:signal:main:states")]
		RailwaySignalMainStates,

		[OSMTagType("railway:signal:position")]
		RailwaySignalPosition,

		[OSMTagType("railway:signal:speed_limit")]
		RailwaySignalSpeedLimit,

		[OSMTagType("railway:signal:speed_limit:form")]
		RailwaySignalSpeedLimitForm,

		[OSMTagType("railway:signal:speed_limit:height")]
		RailwaySignalSpeedLimitHeight,

		[OSMTagType("railway:signal:speed_limit:speed")]
		RailwaySignalSpeedLimitSpeed,

		[OSMTagType("railway:signal:minor")]
		RailwaySignalMinor,

		[OSMTagType("railway:signal:minor:form")]
		RailwaySignalMinorForm,

		[OSMTagType("uic_ref")]
		UicRef,

		[OSMTagType("access")]
		Access,

		[OSMTagType("parking")]
		Parking,

		[OSMTagType("direction")]
		Direction,

		[OSMTagType("tourism")]
		Tourism,

		[OSMTagType("railway:local_operated")]
		RailwayLocalOperated,

		[OSMTagType("railway:switch:electric")]
		RailwaySwitchElectric,

		[OSMTagType("fax")]
		Fax,

		[OSMTagType("fuel:diesel")]
		FuelDiesel,

		[OSMTagType("fuel:octane_95")]
		FuelOctane95,

		[OSMTagType("payment:credit_cards")]
		PaymentCreditCards,

		[OSMTagType("traffic_calming")]
		TrafficCalming,

		[OSMTagType("asb")]
		Asb,

		[OSMTagType("man_made")]
		ManMade,

		[OSMTagType("note:de")]
		NoteDe,

		[OSMTagType("onkz")]
		Onkz,

		[OSMTagType("source")]
		Source,

		[OSMTagType("office")]
		Office,

		[OSMTagType("entrance")]
		Entrance,

		[OSMTagType("covered")]
		Covered,

		[OSMTagType("payment:coins")]
		PaymentCoins,

		[OSMTagType("payment:telephone_cards")]
		PaymentTelephoneCards,

		[OSMTagType("collection_times")]
		CollectionTimes,

		[OSMTagType("disused:shop")]
		DisusedShop,

		[OSMTagType("phone_1")]
		Phone1,

		[OSMTagType("contact:phone")]
		ContactPhone,

		[OSMTagType("contact:website")]
		ContactWebsite,

		[OSMTagType("indoor_seating")]
		IndoorSeating,

		[OSMTagType("clothes")]
		Clothes,

		[OSMTagType("second_hand")]
		SecondHand,

		[OSMTagType("ref:standort")]
		RefStandort,

		[OSMTagType("sms")]
		Sms,

		[OSMTagType("maxheight")]
		Maxheight,

		[OSMTagType("description")]
		Description,

		[OSMTagType("level")]
		Level,

		[OSMTagType("short_name")]
		ShortName,

		[OSMTagType("vending")]
		Vending,

		[OSMTagType("power")]
		Power,

		[OSMTagType("stars")]
		Stars,

		[OSMTagType("bin")]
		Bin,

		[OSMTagType("foot")]
		Foot,

		[OSMTagType("capacity")]
		Capacity,

		[OSMTagType("capacity:disabled")]
		CapacityDisabled,

		[OSMTagType("operator:type")]
		OperatorType,

		[OSMTagType("recycling:shoes")]
		RecyclingShoes,

		[OSMTagType("oven")]
		Oven,

		[OSMTagType("contact:email")]
		ContactEmail,

		[OSMTagType("disused:name")]
		DisusedName,

		[OSMTagType("railway:signal:distant")]
		RailwaySignalDistant,

		[OSMTagType("railway:signal:distant:form")]
		RailwaySignalDistantForm,

		[OSMTagType("railway:signal:distant:height")]
		RailwaySignalDistantHeight,

		[OSMTagType("railway:signal:distant:repeated")]
		RailwaySignalDistantRepeated,

		[OSMTagType("railway:signal:distant:states")]
		RailwaySignalDistantStates,

		[OSMTagType("railway:signal:speed_limit_distant")]
		RailwaySignalSpeedLimitDistant,

		[OSMTagType("railway:signal:speed_limit_distant:form")]
		RailwaySignalSpeedLimitDistantForm,

		[OSMTagType("railway:signal:speed_limit_distant:speed")]
		RailwaySignalSpeedLimitDistantSpeed,

		[OSMTagType("backrest")]
		Backrest,

		[OSMTagType("colour")]
		Colour,

		[OSMTagType("material")]
		Material,

		[OSMTagType("seats")]
		Seats,

		[OSMTagType("bicycle_parking")]
		BicycleParking,

		[OSMTagType("lit")]
		Lit,

		[OSMTagType("smoothness")]
		Smoothness,

		[OSMTagType("supervised")]
		Supervised,

		[OSMTagType("surface")]
		Surface,

		[OSMTagType("healthcare:speciality")]
		HealthcareSpeciality,

		[OSMTagType("self_service")]
		SelfService,

		[OSMTagType("centralkey")]
		Centralkey,

		[OSMTagType("changing_table")]
		ChangingTable,

		[OSMTagType("changing_table:location")]
		ChangingTableLocation,

		[OSMTagType("female")]
		Female,

		[OSMTagType("male")]
		Male,

		[OSMTagType("information")]
		Information,

		[OSMTagType("smokefree")]
		Smokefree,

		[OSMTagType("government")]
		Government,

		[OSMTagType("traffic_signals:sound")]
		TrafficSignalsSound,

		[OSMTagType("traffic_signals:vibration")]
		TrafficSignalsVibration,

		[OSMTagType("check_date:tactile_paving")]
		CheckDateTactilePaving,

		[OSMTagType("blind:description:de")]
		BlindDescriptionDe,

		[OSMTagType("blind:description:en")]
		BlindDescriptionEn,

		[OSMTagType("braille")]
		Braille,

		[OSMTagType("embossed_letters")]
		EmbossedLetters,

		[OSMTagType("lastcheck")]
		Lastcheck,

		[OSMTagType("railway:signal:combined")]
		RailwaySignalCombined,

		[OSMTagType("railway:signal:combined:form")]
		RailwaySignalCombinedForm,

		[OSMTagType("railway:signal:combined:function")]
		RailwaySignalCombinedFunction,

		[OSMTagType("railway:signal:combined:height")]
		RailwaySignalCombinedHeight,

		[OSMTagType("railway:signal:combined:shortened")]
		RailwaySignalCombinedShortened,

		[OSMTagType("railway:signal:combined:states")]
		RailwaySignalCombinedStates,

		[OSMTagType("railway:signal:combined:substitute_signal")]
		RailwaySignalCombinedSubstituteSignal,

		[OSMTagType("fixme")]
		Fixme,

		[OSMTagType("railway:derail")]
		RailwayDerail,

		[OSMTagType("railway:signal:minor:height")]
		RailwaySignalMinorHeight,

		[OSMTagType("railway:signal:minor:states")]
		RailwaySignalMinorStates,

		[OSMTagType("organic")]
		Organic,

		[OSMTagType("ref:shop:num")]
		RefShopNum,

		[OSMTagType("ref:vatin")]
		RefVatin,

		[OSMTagType("craft")]
		Craft,

		[OSMTagType("contact:fax")]
		ContactFax,

		[OSMTagType("location")]
		Location,

		[OSMTagType("toilets")]
		Toilets,

		[OSMTagType("payment:electronic_purses")]
		PaymentElectronicPurses,

		[OSMTagType("railway:milestone:emergency_brake_override")]
		RailwayMilestoneEmergencyBrakeOverride,

		[OSMTagType("cuisine:ice_cream")]
		CuisineIceCream,

		[OSMTagType("cuisine:pizza")]
		CuisinePizza,

		[OSMTagType("internet_access:operator")]
		InternetAccessOperator,

		[OSMTagType("license_classes")]
		LicenseClasses,

		[OSMTagType("old_name")]
		OldName,

		[OSMTagType("sloped_curb")]
		SlopedCurb,

		[OSMTagType("dance:teaching")]
		DanceTeaching,

		[OSMTagType("sport")]
		Sport,

		[OSMTagType("last_checked")]
		LastChecked,

		[OSMTagType("disused:railway")]
		DisusedRailway,

		[OSMTagType("leaf_cycle")]
		LeafCycle,

		[OSMTagType("leaf_type")]
		LeafType,

		[OSMTagType("natural")]
		Natural,

		[OSMTagType("coffee")]
		Coffee,

		[OSMTagType("musical_instrument")]
		MusicalInstrument,

		[OSMTagType("emergency")]
		Emergency,

		[OSMTagType("car")]
		Car,

		[OSMTagType("socket:schuko")]
		SocketSchuko,

		[OSMTagType("socket:type2")]
		SocketType2,

		[OSMTagType("voltage")]
		Voltage,

		[OSMTagType("noexit")]
		Noexit,

		[OSMTagType("check_date:crossing")]
		CheckDateCrossing,

		[OSMTagType("railway:ref")]
		RailwayRef,

		[OSMTagType("railway:station_category")]
		RailwayStationCategory,

		[OSMTagType("railway:signal:stop")]
		RailwaySignalStop,

		[OSMTagType("railway:signal:stop:form")]
		RailwaySignalStopForm,

		[OSMTagType("railway:signal:stop:height")]
		RailwaySignalStopHeight,

		[OSMTagType("postal_code")]
		PostalCode,

		[OSMTagType("railway:signal:electricity")]
		RailwaySignalElectricity,

		[OSMTagType("railway:signal:electricity:form")]
		RailwaySignalElectricityForm,

		[OSMTagType("railway:signal:electricity:height")]
		RailwaySignalElectricityHeight,

		[OSMTagType("railway:signal:electricity:turn_direction")]
		RailwaySignalElectricityTurnDirection,

		[OSMTagType("railway:signal:electricity:type")]
		RailwaySignalElectricityType,

		[OSMTagType("railway:signal:main:substitute_signal")]
		RailwaySignalMainSubstituteSignal,

		[OSMTagType("railway:signal:route")]
		RailwaySignalRoute,

		[OSMTagType("railway:signal:route:form")]
		RailwaySignalRouteForm,

		[OSMTagType("railway:signal:route:states")]
		RailwaySignalRouteStates,

		[OSMTagType("railway:signal:main:deactivated")]
		RailwaySignalMainDeactivated,

		[OSMTagType("railway:signal:main:function")]
		RailwaySignalMainFunction,

		[OSMTagType("railway:signal:stop:caption")]
		RailwaySignalStopCaption,

		[OSMTagType("railway:signal:speed_limit:deactivated")]
		RailwaySignalSpeedLimitDeactivated,

		[OSMTagType("lottery")]
		Lottery,

		[OSMTagType("tobacco")]
		Tobacco,

		[OSMTagType("manufacturer")]
		Manufacturer,

		[OSMTagType("ref:manufacturer_inventory")]
		RefManufacturerInventory,

		[OSMTagType("ref:operator_inventory")]
		RefOperatorInventory,

		[OSMTagType("route_ref")]
		RouteRef,

		[OSMTagType("alt_name")]
		AltName,

		[OSMTagType("fair_trade")]
		FairTrade,

		[OSMTagType("social_facility:for")]
		SocialFacilityFor,

		[OSMTagType("name:de")]
		NameDe,

		[OSMTagType("payment:cash")]
		PaymentCash,

		[OSMTagType("traffic_sign")]
		TrafficSign,

		[OSMTagType("currency:EUR")]
		CurrencyEUR,

		[OSMTagType("payment:notes")]
		PaymentNotes,

		[OSMTagType("check_date")]
		CheckDate,

		[OSMTagType("drive_through")]
		DriveThrough,

		[OSMTagType("service")]
		Service,

		[OSMTagType("map_size")]
		MapSize,

		[OSMTagType("map_type")]
		MapType,

		[OSMTagType("speech_output")]
		SpeechOutput,

		[OSMTagType("denotation")]
		Denotation,

		[OSMTagType("genus")]
		Genus,

		[OSMTagType("genus:de")]
		GenusDe,

		[OSMTagType("contact:facebook")]
		ContactFacebook,

		[OSMTagType("payment:cards")]
		PaymentCards,

		[OSMTagType("artist_name")]
		ArtistName,

		[OSMTagType("artwork_type")]
		ArtworkType,

		[OSMTagType("board_type")]
		BoardType,

		[OSMTagType("dance:style")]
		DanceStyle,

		[OSMTagType("species")]
		Species,

		[OSMTagType("species:de")]
		SpeciesDe,

		[OSMTagType("denomination")]
		Denomination,

		[OSMTagType("religion")]
		Religion,

		[OSMTagType("fire_hydrant:diameter")]
		FireHydrantDiameter,

		[OSMTagType("fire_hydrant:position")]
		FireHydrantPosition,

		[OSMTagType("fire_hydrant:type")]
		FireHydrantType,

		[OSMTagType("advertising")]
		Advertising,

		[OSMTagType("payment:girocard")]
		PaymentGirocard,

		[OSMTagType("unisex")]
		Unisex,

		[OSMTagType("communication:gsm-r")]
		CommunicationGsmR,

		[OSMTagType("railway:radio")]
		RailwayRadio,

		[OSMTagType("tower:type")]
		TowerType,

		[OSMTagType("name:en")]
		NameEn,

		[OSMTagType("waste")]
		Waste,

		[OSMTagType("water_source")]
		WaterSource,

		[OSMTagType("communication:mobile_phone")]
		CommunicationMobilePhone,

		[OSMTagType("reservation")]
		Reservation,

		[OSMTagType("width")]
		Width,

		[OSMTagType("bollard")]
		Bollard,

		[OSMTagType("support")]
		Support,

		[OSMTagType("circumference")]
		Circumference,

		[OSMTagType("max_age")]
		MaxAge,

		[OSMTagType("min_age")]
		MinAge,

		[OSMTagType("fitness_station")]
		FitnessStation,

		[OSMTagType("beauty")]
		Beauty,

		[OSMTagType("network")]
		Network,

		[OSMTagType("route")]
		Route,

		[OSMTagType("type")]
		Type,

		[OSMTagType("from")]
		From,

		[OSMTagType("to")]
		To,

		[OSMTagType("via")]
		Via,

		[OSMTagType("TMC:cid_58:tabcd_1:Class")]
		TMCCid58Tabcd1Class,

		[OSMTagType("TMC:cid_58:tabcd_1:LCLversion")]
		TMCCid58Tabcd1Lclversion,

		[OSMTagType("TMC:cid_58:tabcd_1:LocationCode")]
		TMCCid58Tabcd1Locationcode,

		[OSMTagType("network:metro")]
		NetworkMetro,

		[OSMTagType("network:short")]
		NetworkShort,

		[OSMTagType("public_transport:version")]
		PublicTransportVersion,

		[OSMTagType("admin_level")]
		AdminLevel,

		[OSMTagType("border_type")]
		BorderType,

		[OSMTagType("boundary")]
		Boundary,

		[OSMTagType("de:amtlicher_gemeindeschluessel")]
		DeAmtlicherGemeindeschluessel,

		[OSMTagType("de:place")]
		DePlace,

		[OSMTagType("de:regionalschluessel")]
		DeRegionalschluessel,

		[OSMTagType("license_plate_code")]
		LicensePlateCode,

		[OSMTagType("name:fr")]
		NameFr,

		[OSMTagType("ref:nuts:3")]
		RefNuts3,

		[OSMTagType("destination")]
		Destination,

		[OSMTagType("detour")]
		Detour,

		[OSMTagType("interval")]
		Interval,

		[OSMTagType("restriction")]
		Restriction,

		[OSMTagType("length")]
		Length,

		[OSMTagType("note:route")]
		NoteRoute,

		[OSMTagType("postal_code_level")]
		PostalCodeLevel,

		[OSMTagType("name:prefix")]
		NamePrefix,

		[OSMTagType("int_name")]
		IntName,

		[OSMTagType("osmc:symbol")]
		OsmcSymbol,

		[OSMTagType("pilgrimage")]
		Pilgrimage,

		[OSMTagType("symbol")]
		Symbol,

		[OSMTagType("wiki:symbol")]
		WikiSymbol,

		[OSMTagType("TMC:cid_58:tabcd_1:Direction")]
		TMCCid58Tabcd1Direction,

		[OSMTagType("TMC:cid_58:tabcd_1:NextLocationCode")]
		TMCCid58Tabcd1Nextlocationcode,

		[OSMTagType("TMC:cid_58:tabcd_1:PrevLocationCode")]
		TMCCid58Tabcd1Prevlocationcode,

		[OSMTagType("building")]
		Building,

		[OSMTagType("roundtrip")]
		Roundtrip,

		[OSMTagType("wikipedia:en")]
		WikipediaEn,

		[OSMTagType("wikipedia:fr")]
		WikipediaFr,

		[OSMTagType("route_master")]
		RouteMaster,

		[OSMTagType("enforcement")]
		Enforcement,

		[OSMTagType("note:name")]
		NoteName,

		[OSMTagType("election:parliament")]
		ElectionParliament,

		[OSMTagType("election:part")]
		ElectionPart,

		[OSMTagType("election:year")]
		ElectionYear,

		[OSMTagType("landuse")]
		Landuse,

		[OSMTagType("long_name")]
		LongName,

		[OSMTagType("headway")]
		Headway,

		[OSMTagType("section")]
		Section,

		[OSMTagType("distance")]
		Distance,

		[OSMTagType("heritage")]
		Heritage,

		[OSMTagType("heritage:operator")]
		HeritageOperator,

		[OSMTagType("heritage:website")]
		HeritageWebsite,

		[OSMTagType("name:ca")]
		NameCa,

		[OSMTagType("name:es")]
		NameEs,

		[OSMTagType("name:fi")]
		NameFi,

		[OSMTagType("name:is")]
		NameIs,

		[OSMTagType("name:it")]
		NameIt,

		[OSMTagType("name:nl")]
		NameNl,

		[OSMTagType("name:no")]
		NameNo,

		[OSMTagType("name:pl")]
		NamePl,

		[OSMTagType("ref:whc")]
		RefWhc,

		[OSMTagType("whc:criteria")]
		WhcCriteria,

		[OSMTagType("whc:inscription_date")]
		WhcInscriptionDate,

		[OSMTagType("name:bos")]
		NameBos,

		[OSMTagType("name:hr")]
		NameHr,

		[OSMTagType("name:sl")]
		NameSl,

		[OSMTagType("name:cs")]
		NameCs,

		[OSMTagType("maxspeed")]
		Maxspeed,

		[OSMTagType("maxspeed:type")]
		MaxspeedType,

		[OSMTagType("sidewalk")]
		Sidewalk,

		[OSMTagType("zone:maxspeed")]
		ZoneMaxspeed,

		[OSMTagType("cycleway")]
		Cycleway,

		[OSMTagType("oneway")]
		Oneway,

		[OSMTagType("cycleway:both")]
		CyclewayBoth,

		[OSMTagType("cycleway:left")]
		CyclewayLeft,

		[OSMTagType("cycleway:right")]
		CyclewayRight,

		[OSMTagType("lanes")]
		Lanes,

		[OSMTagType("sidewalk:right:bicycle")]
		SidewalkRightBicycle,

		[OSMTagType("junction")]
		Junction,

		[OSMTagType("toll:N3")]
		TollN3,

		[OSMTagType("zone:traffic")]
		ZoneTraffic,

		[OSMTagType("detail")]
		Detail,

		[OSMTagType("electrified")]
		Electrified,

		[OSMTagType("frequency")]
		Frequency,

		[OSMTagType("gauge")]
		Gauge,

		[OSMTagType("railway:pzb")]
		RailwayPzb,

		[OSMTagType("railway:track_ref")]
		RailwayTrackRef,

		[OSMTagType("usage")]
		Usage,

		[OSMTagType("workrules")]
		Workrules,

		[OSMTagType("lanes:backward")]
		LanesBackward,

		[OSMTagType("lanes:forward")]
		LanesForward,

		[OSMTagType("turn:lanes:forward")]
		TurnLanesForward,

		[OSMTagType("oneway:conditional")]
		OnewayConditional,

		[OSMTagType("source:maxspeed")]
		SourceMaxspeed,

		[OSMTagType("parking:condition:right")]
		ParkingConditionRight,

		[OSMTagType("parking:lane:right")]
		ParkingLaneRight,

		[OSMTagType("tunnel")]
		Tunnel,

		[OSMTagType("passenger_lines")]
		PassengerLines,

		[OSMTagType("old_ref")]
		OldRef,

		[OSMTagType("segregated")]
		Segregated,

		[OSMTagType("bicycle:conditional")]
		BicycleConditional,

		[OSMTagType("vehicle:conditional")]
		VehicleConditional,

		[OSMTagType("cyclestreet")]
		Cyclestreet,

		[OSMTagType("oneway:bicycle")]
		OnewayBicycle,

		[OSMTagType("parking:lane:both")]
		ParkingLaneBoth,

		[OSMTagType("sidewalk:left:bicycle")]
		SidewalkLeftBicycle,

		[OSMTagType("bridge")]
		Bridge,

		[OSMTagType("motorcar")]
		Motorcar,

		[OSMTagType("motorcycle")]
		Motorcycle,

		[OSMTagType("area")]
		Area,

		[OSMTagType("construction")]
		Construction,

		[OSMTagType("parking:condition:both")]
		ParkingConditionBoth,

		[OSMTagType("turn:lanes:backward")]
		TurnLanesBackward,

		[OSMTagType("loc_name")]
		LocName,

		[OSMTagType("intermittent")]
		Intermittent,

		[OSMTagType("salt")]
		Salt,

		[OSMTagType("water")]
		Water,

		[OSMTagType("motor_vehicle")]
		MotorVehicle,

		[OSMTagType("cycleway:surface")]
		CyclewaySurface,

		[OSMTagType("footway:surface")]
		FootwaySurface,

		[OSMTagType("agricultural")]
		Agricultural,

		[OSMTagType("motorroad")]
		Motorroad,

		[OSMTagType("footway")]
		Footway,

		[OSMTagType("crop")]
		Crop,

		[OSMTagType("turn:lanes")]
		TurnLanes,

		[OSMTagType("destination:ref")]
		DestinationRef,

		[OSMTagType("plots")]
		Plots,

		[OSMTagType("heritage:ref")]
		HeritageRef,

		[OSMTagType("heritage:since")]
		HeritageSince,

		[OSMTagType("note:en")]
		NoteEn,

		[OSMTagType("railway:ballastless")]
		RailwayBallastless,

		[OSMTagType("railway:etcs")]
		RailwayEtcs,

		[OSMTagType("railway:lzb")]
		RailwayLzb,

		[OSMTagType("railway:traffic_mode")]
		RailwayTrafficMode,

		[OSMTagType("building:levels")]
		BuildingLevels,

		[OSMTagType("roof:levels")]
		RoofLevels,

		[OSMTagType("maxspeed:forward")]
		MaxspeedForward,

		[OSMTagType("source:maxspeed:forward")]
		SourceMaxspeedForward,

		[OSMTagType("roof:shape")]
		RoofShape,

		[OSMTagType("handrail:left")]
		HandrailLeft,

		[OSMTagType("handrail:right")]
		HandrailRight,

		[OSMTagType("ramp:stroller")]
		RampStroller,

		[OSMTagType("step_count")]
		StepCount,

		[OSMTagType("capacity:women")]
		CapacityWomen,

		[OSMTagType("service_times")]
		ServiceTimes,

		[OSMTagType("maxweight")]
		Maxweight,

		[OSMTagType("moped")]
		Moped,

		[OSMTagType("fence_type")]
		FenceType,

		[OSMTagType("indoor")]
		Indoor,

		[OSMTagType("noaddress")]
		Noaddress,

		[OSMTagType("horse")]
		Horse,

		[OSMTagType("incline")]
		Incline,

		[OSMTagType("tracktype")]
		Tracktype,

		[OSMTagType("social_facility")]
		SocialFacility,

		[OSMTagType("psv")]
		Psv,

		[OSMTagType("lane_markings")]
		LaneMarkings,

		[OSMTagType("maxwidth")]
		Maxwidth,

		[OSMTagType("building:colour")]
		BuildingColour,

		[OSMTagType("building:material")]
		BuildingMaterial,

		[OSMTagType("roof:colour")]
		RoofColour,

		[OSMTagType("roof:material")]
		RoofMaterial,

		[OSMTagType("designation")]
		Designation,

		[OSMTagType("ref:bufa")]
		RefBufa,

		[OSMTagType("roof:height")]
		RoofHeight,

		[OSMTagType("hgv")]
		Hgv,

		[OSMTagType("maxspeed:backward")]
		MaxspeedBackward,

		[OSMTagType("disused")]
		Disused,

		[OSMTagType("drinking_water")]
		DrinkingWater,

		[OSMTagType("destination:ref:to")]
		DestinationRefTo,

		[OSMTagType("cutting")]
		Cutting,

		[OSMTagType("capacity:parent")]
		CapacityParent,

		[OSMTagType("goods")]
		Goods,

		[OSMTagType("hov")]
		Hov,

		[OSMTagType("vehicle")]
		Vehicle,

		[OSMTagType("wikipedia:de")]
		WikipediaDe,

		[OSMTagType("source:addr:housenumber")]
		SourceAddrHousenumber,

		[OSMTagType("roof:orientation")]
		RoofOrientation,

		[OSMTagType("access:conditional")]
		AccessConditional,

		[OSMTagType("highway:conditional")]
		HighwayConditional,

		[OSMTagType("generator:source")]
		GeneratorSource,

		[OSMTagType("park_ride")]
		ParkRide,

		[OSMTagType("ramp")]
		Ramp,

		[OSMTagType("ramp:wheelchair")]
		RampWheelchair,

		[OSMTagType("cycleway:right:lane")]
		CyclewayRightLane,

		[OSMTagType("building:min_level")]
		BuildingMinLevel,

		[OSMTagType("building:height")]
		BuildingHeight,

		[OSMTagType("building:part")]
		BuildingPart,

		[OSMTagType("min_height")]
		MinHeight,

		[OSMTagType("geometry")]
		Geometry,

		[OSMTagType("roof:edge")]
		RoofEdge,

		[OSMTagType("stairwell")]
		Stairwell,

		[OSMTagType("roof:direction")]
		RoofDirection,

		[OSMTagType("addr:interpolation")]
		AddrInterpolation,

		[OSMTagType("wall")]
		Wall,

		[OSMTagType("railway:signal_box")]
		RailwaySignalBox,

		[OSMTagType("bridge:structure")]
		BridgeStructure,

		[OSMTagType("kerb")]
		Kerb
	}
}