using System;
using System.ComponentModel;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.APDU;
using Wiffzack.Devices.CardTerminals.Protocols.ZVT.ApplicationLayer.Parameters;

namespace Cynox.ZvtEcrInterface {
	public enum ZvtCardType {
		DouglasCard = 1,
		EcCardObsolete,
		MilesAndMore,
		RFU,
		Girocard,
		Mastercard,
		EAPS,
		AmericanExpress,
		DebitAdviceBasedOnTrack2OrEMVChip_EuroELV,
		Visa,
		VISAelectron,
		Diners,
		VPAY,
		JCB,
		REKACard,
		Essofleet_card,
		HappinessCards,
		DKV_SVG,
		TransactGeschenkkarte,
		Shellfleet_card,
		Payeasy,
		DEA,
		boncardPOINTS,
		Leaseplan,
		boncardPAY,
		OK,
		Klarmobil,
		UTA,
		MobileWorld,
		Geldkarte_formerlyalso_ec_cashwithChip,
		Ukash,
		Hessol,
		Wallie,
		Lomo,
		MyOne,
		Woehrl,
		GutscheinkarteDOUGLASGruppe,
		Breuninger,
		ABOCard,
		BSW,
		BonusCard,
		ComfortCard,
		CCCCommitCard,
		YESSS,
		DataStandards_DAS,
		Maestro_formerly_edc,
		GiftCard,
		Easycard,
		JelmoliCard,
		CitiShopping,
		J_Geschenkkarte,
		EuroReal_TeleCash,
		Jubin,
		Hertie,
		ManorCard,
		Goertz,
		PowerCard,
		Lafayette,
		Supercardplus,
		Heinemann,
		SwissBonusCard,
		HarleyDavidson,
		SwissCadeau,
		ShoppingPlus,
		Tetora,
		FamilyDentCard,
		WIRcard,
		KarstadtClub,
		Postcard_PostfinanceCard,
		HagebauPartnerCard,
		Lebara,
		Lycamobile,
		GTMobile,
		HP,
		epayGutscheinkarte,
		IKEAFamilyPlus,
		KarstadtBonusCard,
		KochCardPlus,
		Yapital,
		XTRACard,
		Pay_At_Match,
		Optimus,
		Lunch_CheckCard,
		VWClub,
		Tankstellen_Netz_Deutschland,
		Scandlines,
		Bancontact_MisterCash,
		CastCustomer_Card_Payment_function,
		PAYBACKPAY,
		CastCustomer_Card_Bonus_capture,
		ValueMaster,
		ECMcard,
		OrlenFlottenkarte,
		SolitairCard,
		OrlenStar_Card,
		Blauworld,
		ALIPAY,
		REAGutschein_undBonuskarte,
		Roth,
		RothTP,
		EuroWAG,
		Porsche_card,
		ARBÖ_card,
		ÖAMTC_card,
		Netto_App,
		GroupCard,
		ALIPAY_POS_Model2,
		ChequeDejeuner_UPSlovensko,
		CallioGastro,
		DOXX,
		InstantPayment,
		AVIAPrePaidKarte,
		AirPlus = 127,
		HornbachProfi = 137,
		HornbachProjektwelt,
		Weatfleet_card = 142,
		GDBfleet_card = 144,
		DKVbluefleet_card = 146,
		Conoco_Jetfleet_card = 148,
		Gulfcard,
		Eurotraficfleet_card,
		Westfalenfleet_card = 152,
		Elffleet_card = 154,
		Präsentcard,
		Agipfleet_card,
		HornbachGutscheinkarte,
		Totalfleet_card,
		AVIA = 160,
		BFTfleet_card = 162,
		Routexfleet_card = 164,
		PAN_Dieselfleet_card = 166,
		BayWa = 176,
		GAZ_card_Roadrunner_Card,
		Go_Card,
		XNet_Card,
		PaysafeCardBlue,
		PaysafeCardRed,
		Tele2,
		Sunrise,
		SorenaZED,
		Quamnow_card,
		MoxUniversal,
		MoxCallingCard,
		LoopCard,
		GoBananas,
		FreeAndEasycard,
		Callya_Card,
		VCS_DAFA,
		Caravaning_Card,
		AirPlusCargo,
		HEM_card,
		Dankort,
		VISA_Dankort,
		CUP_card,
		Mango_card,
		PaybackPaymentCard,
		LunchCard,
		Payback_withoutpaymentfunction,
		Micromoney,
		T_Card,
		Blau,
		BILDMobil,
		Congstar,
		C3Bestminutes,
		C3Bestcard,
		C3Callingcard,
		EDEKAMOBIL,
		XTRA_PIN,
		Klimacard,
		ICP_International_Fleet_Card,
		ICP_Gutscheinkarte,
		ICP_Bonuskarte,
		AustriaCard,
		ConCardisGeschenkkarte,
		TeleCashGutscheinkarte,
		Shellprivatelabelcreditcard,
		ADAC,
		ShellClubsmart,
		ShellPre_Paid_Card,
		ShellMaster_Card,
		bauMaxZahlkarte,
		Fiat_Lancia_AlfaServicecard,
		Nissan_Karte,
		ÖBBVorteilskarte,
		ÖsterreichTicket,
		Shopin_Karte,
		Tlapa_Karte,
		DiscoverCard,
		fundf_Karte_FreiUndFlott_Karte,
		Syrcon,
		CitybikeCard,
		IKEAFAMILYBezahlkarte = 238,
		IkanoShoppingCard,
		IntercardGutscheinkarte,
		IntercardKundenkarte,
		MAndM_Gutscheinkarte,
		Montradacard,
		CPCustomerCard,
		AmexMembershipReward,
		FONIC,
		OTELODE,
		SIMYO,
		SchleckerSmobil,
		SchleckerZusatzprodukte,
		CHRISTGutscheinkarte,
		IQ_Card,
		AVSGutscheinkarte_Pontos,
		NovofleetCard,
		IndicationforZvtCardTypeIdInTlvTag41
	}

	/// <summary>
	/// High level representation of the StatusInformationApdu data.
	/// </summary>
	public class ZvtStatusInformation {
		public StatusCodes.ErrorIDEnum? Status { get; }
		public long? Amount { get; }
		public long? TraceNr { get; }
		public long? OriginalTraceNr { get; }
		public long? TerminalId { get; }
		public long? SequencNumber { get; }
		public long? ReceiptNr { get; }
		public StatusPaymentTypeParam.PaymentTypeEnum? PaymentType { get; }
		public ZvtCardType? CardType { get; }
		public byte? CardTypeId { get; }
		public string CardTypeName { get; }
		public string AdditionalTextForCreditCards { get; }
		public string ContractNumberForCreditCards { get; }
		public long? TurnOverNumber { get; }
		public string PanEfId { get; }

		[Browsable(false)]
		public StatusInformationApdu SourceApdu { get; }

		/// <summary>
		/// Only year and month is provided from transaction-data and thus relevant.
		/// </summary>
		public DateTime? ExpirationDate { get; }

		/// <summary>
		/// The actual year is initialized to DateTime.Now.Year, since the transaction-data contains no year component. 
		/// </summary>
		public DateTime? Time { get; }

		public ZvtStatusInformation(StatusInformationApdu statusInformation) {
			if (statusInformation == null) {
				return;
			}

			SourceApdu = statusInformation;

			var statusParam = statusInformation.FindParameter<StatusInformationResultCode>(StatusInformationApdu.StatusParameterEnum.ResultCode);
			Status = statusParam?.ResultCode;

			var amountParam = statusInformation.FindParameter<BCDNumberParameter>(StatusInformationApdu.StatusParameterEnum.Amount);
			Amount = amountParam?.DecodeNumber();

			var traceNrParam = statusInformation.FindParameter<BCDNumberParameter>(StatusInformationApdu.StatusParameterEnum.TraceNr);
			TraceNr = traceNrParam?.DecodeNumber();

			var originalTraceNrParam = statusInformation.FindParameter<BCDNumberParameter>(StatusInformationApdu.StatusParameterEnum.OriginalTraceNr);
			OriginalTraceNr = originalTraceNrParam?.DecodeNumber();

			var timeParam = statusInformation.FindParameter<StatusTimeParameter>(StatusInformationApdu.StatusParameterEnum.Time);
			var dateParam = statusInformation.FindParameter<StatusDateParameter>(StatusInformationApdu.StatusParameterEnum.Date);

			if (timeParam != null && dateParam != null) {
				Time = new DateTime(DateTime.Now.Year, dateParam.Month, dateParam.Day, timeParam.Hour, timeParam.Minute, timeParam.Second);
			} else {
				Time = null;
			}

			var expiryDateParam = statusInformation.FindParameter<StatusExpDateParameter>(StatusInformationApdu.StatusParameterEnum.ExpiryDate);

			if (expiryDateParam != null) {
				int actualYear = expiryDateParam.Year + 2000;
				int lastDayInMonth = DateTime.DaysInMonth(actualYear, expiryDateParam.Month);
				ExpirationDate = new DateTime(actualYear, expiryDateParam.Month, lastDayInMonth, 23, 59, 59);
			} else {
				ExpirationDate = null;
			}

			var terminalIdParam = statusInformation.FindParameter<BCDNumberParameter>(StatusInformationApdu.StatusParameterEnum.TerminalId);
			TerminalId = terminalIdParam?.DecodeNumber();

			var sequenceNumberParam = statusInformation.FindParameter<BCDNumberParameter>(StatusInformationApdu.StatusParameterEnum.SequenceNumber);
			SequencNumber = sequenceNumberParam?.DecodeNumber();

			var receiptNumberParam = statusInformation.FindParameter<BCDNumberParameter>(StatusInformationApdu.StatusParameterEnum.ReceiptNr);
			ReceiptNr = receiptNumberParam?.DecodeNumber();

			var paymentTyperParam = statusInformation.FindParameter<StatusPaymentTypeParam>(StatusInformationApdu.StatusParameterEnum.PaymentType);
			PaymentType = paymentTyperParam?.PaymentType;

			var panEfId = statusInformation.FindParameter<StatusPanEfId>(StatusInformationApdu.StatusParameterEnum.PanEfId);
			PanEfId = panEfId?.DecodeNumberAsString();

			var cardTypeParam = statusInformation.FindParameter<SingleByteParameter>(StatusInformationApdu.StatusParameterEnum.CardType);
			if (cardTypeParam?.MyByte != null) {
				CardType = (ZvtCardType)cardTypeParam.MyByte;
			} 

			var cardTypeIdParam = statusInformation.FindParameter<SingleByteParameter>(StatusInformationApdu.StatusParameterEnum.CardTypeID);
			CardTypeId = cardTypeIdParam?.MyByte;

			var cardTypeNameParam = statusInformation.FindParameter<AsciiLVarParameter>(StatusInformationApdu.StatusParameterEnum.CardTypeName);
			CardTypeName = cardTypeNameParam?.Text;

			var additionalTextForCcParam = statusInformation.FindParameter<AsciiLVarParameter>(StatusInformationApdu.StatusParameterEnum.AdditionalTextForCC);
			AdditionalTextForCreditCards = additionalTextForCcParam?.Text;

			var contactNumberForCcParam = statusInformation.FindParameter<AsciiFixedSizeParameter>(StatusInformationApdu.StatusParameterEnum.ContractNumberForCC);
			ContractNumberForCreditCards = contactNumberForCcParam?.Text;

			var turnOverNrParam = statusInformation.FindParameter<BCDNumberParameter>(StatusInformationApdu.StatusParameterEnum.TurnoverNr);
			TurnOverNumber = turnOverNrParam?.DecodeNumber();
		}
	}
}
