﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StructuedContracttables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractTemplate");

            migrationBuilder.DropTable(
                name: "StaticContractTemplates");

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilitySignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanySignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true),
                    OfferId = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contracts_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contracts_PriceRequestOffers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "PriceRequestOffers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Key",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Key", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticClauses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticClauses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaticContracts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreambleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreambleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClosingRemarkAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClosingRemarkEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticContracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomClause",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HtmlContentAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtmlContentEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorType = table.Column<int>(type: "int", nullable: true),
                    ContractId = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomClause", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomClause_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractKey",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<long>(type: "bigint", nullable: false),
                    KeyId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractKey = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractKey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractKey_Contracts_ContractKey",
                        column: x => x.ContractKey,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractKey_Key_KeyId",
                        column: x => x.KeyId,
                        principalTable: "Key",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractKey_ContractKey",
                table: "ContractKey",
                column: "ContractKey");

            migrationBuilder.CreateIndex(
                name: "IX_ContractKey_KeyId",
                table: "ContractKey",
                column: "KeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CompanyId",
                table: "Contracts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_FacilityId",
                table: "Contracts",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OfferId",
                table: "Contracts",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomClause_ContractId",
                table: "CustomClause",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractKey");

            migrationBuilder.DropTable(
                name: "CustomClause");

            migrationBuilder.DropTable(
                name: "StaticClauses");

            migrationBuilder.DropTable(
                name: "StaticContracts");

            migrationBuilder.DropTable(
                name: "Key");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.CreateTable(
                name: "ContractTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferId = table.Column<long>(type: "bigint", nullable: false),
                    ContractJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    PartyOneSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyTwoSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractTemplate_PriceRequestOffers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "PriceRequestOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaticContractTemplates",
                columns: table => new
                {
                    Version = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JsonData = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticContractTemplates", x => x.Version);
                });

            migrationBuilder.InsertData(
                table: "StaticContractTemplates",
                columns: new[] { "Version", "JsonData" },
                values: new object[] { 1, "{\n  \"ContractTitle\": {\n    \"Ar\": \"عقد تقديم خدمات الحراسات الأمنية المدنية الخاصة\",\n    \"En\": \"Contract for the Provision of Private Civil Security Guard Services\"\n  },\n  \"Preamble\": {\n    \"Title\": {\n      \"Ar\": \"أبرم هذا العقد بين كل من\",\n      \"En\": \"This contract is made between\"\n    },\n    \"Parties\": {\n      \"FirstParty\": {\n        \"Description\": {\n          \"Ar\": \"الطرف الأول: شركة {{PartyOne.Name.Ar}} ومركزها الرئيسي في مدينة {{PartyOne.MainOfficeCity.Ar}} مسجــلة بالسجل التجاري رقم ({{PartyOne.CommercialRegistration}}) ترخيص الأمن العام رقم {{PartyOne.PublicSecurityLicense}} هاتف {{PartyOne.Telephone}} جوال {{PartyOne.Mobile}} العنوان الوطني {{PartyOne.NationalAddress.City.Ar}} الرمز البريدي ({{PartyOne.NationalAddress.PostalCode}}) وحدة رقم ({{PartyOne.NationalAddress.UnitNumber}}) مبنى رقم ({{PartyOne.NationalAddress.BuildingNumber}}) رقم التسجيل في سبل، واصل ({{PartyOne.RegistrationInSabl}}) بريد إلكتروني {{PartyOne.Email}} ويمثلها في التوقيع على هذا العقد  / {{PartyOne.RepresentativeName.Ar}} بصفته {{PartyOne.RepresentativeTitle.Ar}}.\",\n          \"En\": \"First Party: Company {{PartyOne.Name.En}} with its headquarters in {{PartyOne.MainOfficeCity.En}}, registered under Commercial Registration No. ({{PartyOne.CommercialRegistration}}), Public Security License No. {{PartyOne.PublicSecurityLicense}}, Phone {{PartyOne.Telephone}}, Mobile {{PartyOne.Mobile}}, National Address {{PartyOne.NationalAddress.City.En}}, Postal Code ({{PartyOne.NationalAddress.PostalCode}}), Unit No. ({{PartyOne.NationalAddress.UnitNumber}}), Building No. ({{PartyOne.NationalAddress.BuildingNumber}}), registered in Wasel as ({{PartyOne.RegistrationInSabl}}), Email {{PartyOne.Email}}. Represented by {{PartyOne.RepresentativeName.En}} as {{PartyOne.RepresentativeTitle.En}}.\"\n        }\n      },\n      \"SecondParty\": {\n        \"Description\": {\n          \"Ar\": \"الطرف الثاني: السادة/ {{PartyTwo.Name.Ar}} العقد الأساسي الموقع بتاريخ {{ContractSignDate}} ومركزها الرئيسي في مدينة {{PartyTwo.MainOfficeCity.Ar}} : مسجــلة بالسجل التجاري مسجل بمدينة {{PartyTwo.CommercialRegistrationCity.Ar}} ({{PartyTwo.CommercialRegistration}}) جوال ({{PartyTwo.Mobile}}) العنوان الوطني ({{PartyTwo.NationalAddress.City.Ar}}) الرمز البريدي ({{PartyTwo.NationalAddress.PostalCode}}) وحدة رقم ({{PartyTwo.NationalAddress.UnitNumber}}) مبنى رقم ({{PartyTwo.NationalAddress.BuildingNumber}}) بريد إلكتروني {{PartyTwo.Email}} ويمثلها في التوقيع على هذا العقد الأستاذ / {{PartyTwo.RepresentativeName.Ar}} بصفته :  {{PartyTwo.RepresentativeTitle.Ar}}.\",\n          \"En\": \"Second Party: {{PartyTwo.Name.En}} with the main contract signed on {{ContractSignDate}} and its headquarters in {{PartyTwo.MainOfficeCity.En}}. Registered in the Commercial Registry in the city of {{PartyTwo.CommercialRegistrationCity.En}} ({{PartyTwo.CommercialRegistration}}), Mobile ({{PartyTwo.Mobile}}), National Address ({{PartyTwo.NationalAddress.City.En}}), Postal Code ({{PartyTwo.NationalAddress.PostalCode}}), Unit No. ({{PartyTwo.NationalAddress.UnitNumber}}), Building No. ({{PartyTwo.NationalAddress.BuildingNumber}}), Email {{PartyTwo.Email}}. Represented by {{PartyTwo.RepresentativeName.En}} as {{PartyTwo.RepresentativeTitle.En}}.\"\n        }\n      }\n    },\n    \"Introduction\": {\n      \"Ar\": \"حيث أن الطرف الثاني يرغب في تأمين خدمات الحراسة الأمنية المدنية لموقعه {{PartyTwo.LocationToBeSecured.Ar}} فتقدم الطرف الأول بعرضه رقـــــــم ({{OfferNumber}}) وتاريخ ({{OfferDate}}) المرفق به بيان الخدمات التي يقدمها الطرف الأول وأسلوبها وأسعارها وقد لقي العرض قبولاً لدى الطرف الثاني وعليه فقد اتفق الطرفان وتراضيا على البنود والشروط التالية:\",\n      \"En\": \"Whereas the Second Party desires to secure security guard services for its location {{PartyTwo.LocationToBeSecured.En}}, the First Party submitted its offer No. ({{OfferNumber}}) dated ({{OfferDate}}), including the statement of services offered, their method, and pricing. The offer was accepted by the Second Party. Therefore, both parties have agreed to the following terms and conditions:\"\n    },\n    \"Conditions\": [\n      {\n        \"Ar\": \"عدم فسخ العقد من قبل الطرفين الا بعد أشعار شعبة الحراسات الأمنية بالميدانية بذالك.\",\n        \"En\": \"\"\n      },\n      {\n        \"Ar\": \"الالتزام بعمل جدول موضح به عدد الحراسات وأوقات ساعات العمل والتقيد بالتعليمات فيما يخص نظام مكتب العمل والعمال من حيث عدد ساعات العمل بشهر رمضان المبارك.\",\n        \"En\": \"\"\n      }\n    ]\n  },\n  \"Duties_Services\": [\n    {\n      \"Ar\": \"المعرفة التامة بإجراءات الإيقاف وتبليغ السلطات المختصة عند الضرورة والتحفظ على المشتبه فيهم داخل حدود الموقع.\",\n      \"En\": \"Full knowledge of detention procedures and notifying the competent authorities when necessary, and detaining suspects within the site boundaries.\"\n    },\n    {\n      \"Ar\": \"تنظيم حركة دخول وخروج رواد الموقع وضبط ورقابة المداخل والمخارج.\",\n      \"En\": \"Organizing the movement of site visitors entering and exiting, and controlling and monitoring the entrances and exits.\"\n    },\n    {\n      \"Ar\": \"المعرفة التامة بكيفية التعامل مع المعدات الأمنية المتاحة وأمثل الطرق لاستخدامها.\",\n      \"En\": \"Full knowledge of how to use the available security equipment and the best methods of utilizing them.\"\n    },\n    {\n      \"Ar\": \"استيعاب تعليمات الأمن الدائمة والالتزام بتنفيذها.\",\n      \"En\": \"Understanding and adhering to permanent security instructions.\"\n    },\n    {\n      \"Ar\": \"إجادة أسلوب التعامل مع الآخرين في إطار العلاقات العامة والاستقبال.\",\n      \"En\": \"Proficiency in dealing with others within the framework of public relations and reception.\"\n    },\n    {\n      \"Ar\": \"المحافظة على مواعيد العمل حضوراً وانصرافاً مع ضرورة التواجد على رأس العمل أثناء ساعات الدوام الرسمية.\",\n      \"En\": \"Maintaining punctuality for attendance and departure, and being present at the workplace during official working hours.\"\n    },\n    {\n      \"Ar\": \"الالتزام بارتداء الزي الرسمي الخاص برجال الأمن وعليه شعار الطرف الأول في جميع أوقات العمل والمحافظة على نظافة وحسن المظهر العام.\",\n      \"En\": \"Adhering to wearing the official security uniform with the first party's logo at all times during work and maintaining cleanliness and a good general appearance.\"\n    },\n    {\n      \"Ar\": \"حمل المعدات الشخصية والضرورية التي تطلبها ضرورة العمل.\",\n      \"En\": \"Carrying the personal and necessary equipment required for work.\"\n    },\n    {\n      \"Ar\": \"القيام بواجبات الحراسة الأمنية الراجلة والراكبة وكافة ما يتطلب عمل الحارس الأمني من واجب المراقبة والمتابعة والانتباه للمواقع التي يحددها الطرف الثاني على مدار عمله.\",\n      \"En\": \"Performing foot and mobile security guard duties, including all monitoring, surveillance, and attentiveness required by the security guard's role at locations specified by the second party throughout working hours.\"\n    },\n    {\n      \"Ar\": \"تقديم المساعدة والإسعافات الأولية والإرشادات لرواد الموقع وتوجيههم في حالة طلبهم ذلك وتنظيم عملية الدخول والخروج في الأماكن الغير مسموح بدخولها وإغلاق الأبواب ومنع الدخول عند انتهاء المواعيد المحددة من قبل الطرف الثاني وتشغيل وإطفاء الإنارة وقت الحاجة.\",\n      \"En\": \"Providing assistance, first aid, and guidance to site visitors upon request, organizing entry and exit in restricted areas, closing doors, preventing access after the times specified by the second party, and operating and turning off lights as needed.\"\n    },\n    {\n      \"Ar\": \"تولي المسئولية لدى حدوث أي مشاجرات أو مشاكل داخل الموقع والعمل على منع تطورها أو حلها أو رفعها لممثل الطرف الثاني.\",\n      \"En\": \"Taking responsibility in case of any disputes or problems within the site, working to prevent their escalation, resolving them, or reporting them to the second party's representative.\"\n    },\n    {\n      \"Ar\": \"اتخاذ كافة الاحتياطات وإجراءات السلامة اللازمة والتوجه بها للطرف الثاني لدرء الأخطار المحدقة مثل (إبعاد أي مواد أو أجهزة مشتبه فيها أو قد ينشأ من وجودها خطر حريق أو انفجار).\",\n      \"En\": \"Taking all necessary precautions and safety measures and reporting them to the second party to prevent imminent dangers, such as removing any suspicious materials or devices that may pose a fire or explosion risk.\"\n    },\n    {\n      \"Ar\": \"تدقيق وفحص التصاريح والوثائق والمستندات الخاصة بالتحكم في دخول المبنى ومراقبة سلامته وإنارته مثل (شاشات المراقبة – أجهزة الإنذار – الأبواب – أجهزة الاتصال – الإضاءة).\",\n      \"En\": \"Verifying and inspecting permits, documents, and paperwork related to controlling building access and monitoring its safety and lighting, including (surveillance screens, alarm systems, doors, communication devices, and lighting).\"\n    },\n    {\n      \"Ar\": \"اتخاذ ما يلزم عند حدوث أي تحرك أو نشاط مشبوه أو أية مشاكل أو أحداث غير طبيعية داخل أو حول المواقع من قبل أي شخص أو مجموعات والإبلاغ الفوري عنها.\",\n      \"En\": \"Taking necessary actions when any suspicious movement or activity, problems, or unusual events occur inside or around the sites by any person or groups, and immediately reporting them.\"\n    },\n    {\n      \"Ar\": \"يلتزم الطرف الأول بالتأكد من قيام حراس الأمن التابعين له بمهام الحراسة المطلوبة منهم على مدار الساعة.\",\n      \"En\": \"The first party is committed to ensuring that its security guards perform their required security duties around the clock.\"\n    },\n    {\n      \"Ar\": \"يلتزم حراس الأمن بعدم الإفشاء عن أي معلومات أو وقائع تصل إليهم أثناء تواجدهم في الموقع لأي جهة أخرى.\",\n      \"En\": \"Security guards are committed to maintaining confidentiality and not disclosing any information or incidents they encounter while on-site to any other party.\"\n    },\n    {\n      \"Ar\": \"يلتزم الطرف الأول بتحمل كافة الالتزامات التي تنشأ عند تشغيله لحراس الأمن ويلتزم بدفع رواتبهم في مواعيدها حيث لن يقبل الطرف الثاني أية مطالبة مباشرة منهم ويتحمل الطرف الأول كافة الالتزامات المالية المتعلقة بهم ويستوفي كافة الإجراءات اللازمة لتشغيلهم.\",\n      \"En\": \"The first party is committed to bearing all obligations arising from employing security guards and paying their salaries on time, as the second party will not accept any direct claims from them. The first party assumes all financial obligations related to them and completes all necessary employment procedures.\"\n    },\n    {\n      \"Ar\": \"لا يحق للطرف الثاني إعطاء موظفي الطرف الأول رواتبهم أو سلفه مالية وإذا قام بذلك لا يحق له خصمها من مستحقات الطرف الأول.\",\n      \"En\": \"The second party is not entitled to pay salaries or give cash advances to the first party's employees. If they do, they cannot deduct them from the first party's dues.\"\n    },\n    {\n      \"Ar\": \"يقوم الطرف الأول بتغطية المواقع في حالة العطلات والأعياد والجمع والإجازات.\",\n      \"En\": \"The first party is responsible for covering the sites during holidays, festive occasions, Fridays, and vacations.\"\n    }\n  ],\n  \"Clauses\": [\n    {\n      \"Number\": 1,\n      \"Title\": {\n        \"Ar\": \"البند الأول\",\n        \"En\": \"Clause One\"\n      },\n      \"Ar\": \"يعتبر التمهيد السابق جزءاً لا يتجزأ من هذا العقد ومكملاً ومفسراً له وكذلك أي ملاحق يتفق عليها الطرفان.\",\n      \"En\": \"The previous preamble is considered an integral part of this contract and complements and interprets it, as well as any appendices agreed upon by both parties.\"\n    },\n    {\n      \"Number\": 2,\n      \"Title\": {\n        \"Ar\": \"البند الثاني\",\n        \"En\": \"Clause Two\"\n      },\n      \"Ar\": \"إن الغرض من العقد هو قيام الطرف الاول بتأمين وتقديم خدمة الحراسة الأمنية المدنية الخاصة للطرف الثاني.\",\n      \"En\": \"The purpose of this contract is for the first party to provide and ensure private civil security services for the second party.\"\n    },\n    {\n      \"Number\": 3,\n      \"Title\": {\n        \"Ar\": \"البند الثالث\",\n        \"En\": \"Clause Three\"\n      },\n      \"Ar\": \"يعتبر عرض السعر المقدم من الطرف الأول من وثائق العقد الأساسية لما فيها من بيان للخدمات التي يقدمها و أسعارها , كما يعتبر كل تعميد مقدم من الطرف الثاني إلى الطرف الأول خلال مدة العقد من وثائق العقد الأساسية لما فيها من بيان لأعداد أفراد الأمن ومواقع الخدمة.\",\n      \"En\": \"The price quotation submitted by the first party is considered a fundamental document of the contract as it details the services offered and their prices. Additionally, any authorization given by the second party to the first party during the contract period is regarded as a core document detailing the number of security personnel and service locations.\"\n    },\n    {\n      \"Number\": 4,\n      \"Title\": {\n        \"Ar\": \"البند الرابع\",\n        \"En\": \"Clause Four\"\n      },\n      \"Ar\": \"مدة العقد سنة ميلادية (من عرض السعر) قابلة للتجديد لمدة أو مدد مماثلة، ويحق لأي طرف فسخ أو إلغاء هذا العقد شرط إشعار الطرف الأخر خطياً برغبته قبل (60) ستون يوماً من تاريخ الفسخ أو الإلغاء الفعلي للعقد مع دفع جميع المستحقات حتى نهاية الإشعار.\",\n      \"En\": \"The contract duration is one Gregorian year (from the date of the price quotation) and is renewable for the same period or periods. Either party has the right to terminate or cancel this contract by notifying the other party in writing at least sixty (60) days prior to the actual termination or cancellation date, with all dues being settled up to the end of the notification period.\"\n    },\n    {\n      \"Number\": 5,\n      \"Title\": {\n        \"Ar\": \"البند الخامس\",\n        \"En\": \"Clause Five\"\n      },\n      \"Ar\": \"إن مؤسسة {{PartyOne.Name.Ar}} تؤمن خدماتها طبقاً للأنظمة المعمول بها في المملكة العربية السعودية وفي حالة صدور أنظمة أو تعليمات جديدة تتعلق بالخدمات موضوع العقد مباشرة أو غير مباشرة تحتفظ {{PartyOne.Name.Ar}}  بالحق في تعديل قيمة العقد وفقاً للأنظمة والتعليمات الجديدة وذلك بعد الرجوع إلى الطرف الثاني. وخاصه في أنظمة وزارة الموارد البشرية الخاصة بــ مكتب العمل وتحديثات أنظمة ساعات العمل.\",\n      \"En\": \"{{PartyOne.Name.En}} provides its services in accordance with the regulations applicable in the Kingdom of Saudi Arabia. In case of issuance of new regulations or instructions directly or indirectly related to the services under the contract, {{PartyOne.Name.En}} reserves the right to adjust the contract value in accordance with the new regulations and instructions after consulting with the second party, particularly regarding the regulations of the Ministry of Human Resources related to the Labor Office and updates to the working hours systems.\"\n    },\n    {\n      \"Number\": 6,\n      \"Title\": {\n        \"Ar\": \"البند السادس\",\n        \"En\": \"Clause Six\"\n      },\n      \"Ar\": \"يبدأ العقد اعتباراً من تاريخ مباشرة العمل بتاريخ {{ContractStartDate}}.\",\n      \"En\": \"The contract begins from the commencement date of work on {{ContractStartDate}}.\"\n    },\n    {\n      \"Number\": 7,\n      \"Title\": {\n        \"Ar\": \"البند السابع\",\n        \"En\": \"Clause Seven\"\n      },\n      \"SubClauses\": [\n        {\n          \"Number\": 1,\n          \"Ar\": \"حرر هذا العقد وفقاً لما جاء بأمر مقام وزير الداخلية (الأمن العام والقاضي بسعودة الحراسات الأمنية) وذلك بإحلال السعوديين بدلاً من رجال الأمن الغير سعوديين، حيث أننا مؤسسة تقدم الكوادر السعودية مائة بالمائة ومدربة كل في تخصصه.\",\n          \"En\": \"This contract is issued in accordance with the directive of the Minister of Interior (Public Security), which mandates the Saudization of security services by replacing non-Saudi security personnel with Saudi nationals. Our institution provides 100% Saudi personnel who are professionally trained in their respective specializations.\"\n        },\n        {\n          \"Number\": 2,\n          \"Ar\": \"هذا العقد يلغي ما قبله من العقود إن وجدت.\",\n          \"En\": \"This contract supersedes any previous contracts, if they exist.\"\n        },\n        {\n          \"Number\": 3,\n          \"Ar\": \"يلتزم الطرف الاول بتأمين الخدمات الأمنية الخاصة للطرف الثاني في موقعه {{PartyTwo.LocationToBeSecured.Ar}} بواقع فقط عدد ({{PartyOne.GuardsCount}}) رجال أمن لا غير، على مستوى من الكفاءة والخبرة في المجال الأمني.\",\n          \"En\": \"The first party is committed to providing private security services to the second party at their location {{PartyTwo.LocationToBeSecured.En}}, with a total of ({{PartyOne.GuardsCount}}) security guards only, possessing high efficiency and experience in the security field.\"\n        },\n        {\n          \"Number\": 4,\n          \"Ar\": \"يلتزم الطرف الأول بتأمين الخدمات الأمنية الخاصة على مدار ساعات العمل التي يحددها الطرف الثاني على أن لا تتجاوز عدد الساعات عن ثمان ساعات عمل لكل رجل أمن واحد.\",\n          \"En\": \"The first party is committed to providing private security services during the working hours specified by the second party, ensuring that the working hours do not exceed eight hours per security guard.\"\n        }\n      ]\n    },\n    {\n      \"Number\": 8,\n      \"Title\": {\n        \"Ar\": \"البند الثامن : صفات رجال الامن\",\n        \"En\": \"Clause Eight: Security Personnel Qualifications\"\n      },\n      \"Ar\": \"تقوم {{PartyOne.Name.Ar}} بتعيين رجال أمن على مستوى مميز من التدريب وحيث أن المجموعة لديها مراكز للتدريب والتأهيل والتطوير كل حسب اختصاصه للقيام بتوفير الخدمات الأمنية الخاصة للمواقع وفقاً للشروط والتعليمات الواردة من وزارة الداخلية والتوجيهات الصادرة من الجهات الرسمية فيما يتعلق بالخدمات الأمنية وعلى الأخص أن يكون حسن السيرة والسلوك واللياقة الطبية والبدنية والقدرة على القيام بجميع الإجراءات والأعمال الأمنية الخاصة والدوريات على أعلى مستوى.\",\n      \"En\": \"{{PartyOne.Name.En}} appoints security personnel with exceptional levels of training. The group has specialized training, qualification, and development centers to provide specialized security services for sites in accordance with the conditions and instructions issued by the Ministry of Interior and official authorities regarding security services. In particular, security personnel must possess good conduct and behavior, medical and physical fitness, and the ability to perform all special security procedures and patrol duties at the highest level.\"\n    },\n    {\n      \"Number\": 9,\n      \"Title\": {\n        \"Ar\": \"البند التاسع\",\n        \"En\": \"Clause Nine\"\n      },\n      \"Ar\": \"على الطرف الاول تقديم خدمة الإشراف والمتابعة والرقابة لعناصره في الموقع من خلال الاتصالات الدورية والزيارات المفاجئة للتأكد من تواجدهم وحسن أدائهم للعمل.\",\n      \"En\": \"The first party shall provide supervision, follow-up, and monitoring services for its personnel on-site through regular communication and unannounced visits to ensure their presence and the quality of their work performance.\"\n    },\n    {\n      \"Number\": 10,\n      \"Title\": {\n        \"Ar\": \"البند العاشر\",\n        \"En\": \"Clause Ten\"\n      },\n      \"Ar\": \"يقوم رجال الأمن بإتباع التعليمات الواردة من مسئولي الطرف الثاني بالتنسيق مع الطرف الاول في هذا الخصوص وتتمثل مهام رجل الأمن في حراسة الموقع ومنع دخول الأشخاص الغير مصرح لهم وإبلاغ الجهات المسئولة في الحالات التي تستدعي تبليغ الجهات الرسمية كالدفاع المدني أو الشرطة.\",\n      \"En\": \"Security personnel shall follow the instructions provided by the representatives of the second party in coordination with the first party. Their duties include guarding the site, preventing unauthorized access, and notifying the relevant authorities in situations requiring official notification, such as emergencies involving civil defense or police.\"\n    },\n    {\n      \"Number\": 11,\n      \"Title\": {\n        \"Ar\": \"البند الحادي عشر\",\n        \"En\": \"Clause Eleven\"\n      },\n      \"Ar\": \"يلتزم الطرف الأول بتأمين وتوفير رجال الأمن للموقع المشار إليه في المقدمة يتم احتساب عدد ساعات الوردية بواقع (8) ساعات يوميا , (6) ستة أيام في الأسبوع باستثناء شهر رمضان المبارك حيث يتم احتساب عدد ساعات الوردية بما لا يزيد عن ستة ساعات في اليوم ويتم احتساب الساعتين الإضافيتين كعمل إضافي ويتم توضيحه في فاتورة الخدمة.\",\n      \"En\": \"The first party is committed to providing security personnel for the site mentioned in the preamble. The shift hours are calculated as (8) hours per day, (6) days per week, except during the holy month of Ramadan, where shift hours do not exceed six hours per day. Any additional two hours will be considered overtime and will be detailed in the service invoice.\"\n    },\n    {\n      \"Number\": 12,\n      \"Title\": {\n        \"Ar\": \"البند الثاني عشر\",\n        \"En\": \"Clause Twelve\"\n      },\n      \"Ar\": \"بند تكلفة الخدمة المقدمة للطرف الثاني حسب الجدول ادناه :\",\n      \"En\": \"The cost of the service provided to the second party is as per the table below:\"\n    },\n    {\n      \"Number\": 13,\n      \"Title\": {\n        \"Ar\": \"البند الثالث عشر\",\n        \"En\": \"Clause Thirteen\"\n      },\n      \"SubClauses\": [\n        {\n          \"Number\": 1,\n          \"Ar\": \"يقوم الطرف الثاني بالتأمين على ممتلكاته ضد الدخول للموقع للغير المصرح لهم بالدخول او ليس من منسوبي الشركة او الموقع المحدد او السطو علي الموقع  والتبليغ للجهات المختصة في حال نشوب حرائق.\",\n          \"En\": \"The second party is responsible for insuring its property against unauthorized access or entry by non-employees of the company or specified site and against burglary. The second party must also notify the relevant authorities in case of fire incidents.\"\n        },\n        {\n          \"Number\": 2,\n          \"Ar\": \"لا يحق التعاقد مع أي موظف تابع لشركة  {{PartyOne.Name.Ar}} أو توظيفه إلا بعد انقضاء ستة أشهر من تركه الخدمة بالمؤسسة، وفي حال الإخلال بهذا البند يجب دفع ثلاث رواتب عن كل موظف تم تعينه بهذه الطريقة.\",\n          \"En\": \"No employee of {{PartyOne.Name.En}} may be hired or contracted by the second party until six months have passed since the employee left the company. If this clause is violated, three months' salaries must be paid for each employee hired in this manner.\"\n        },\n        {\n          \"Number\": 3,\n          \"Ar\": \"يلتزم الطرف الثاني بتأمين موقع أو مقر خارجي حسب المواصفات المتفق عليها لرجل الأمن ووسيلة اتصال إذا كان الموقع خارجي.\",\n          \"En\": \"The second party is obligated to provide a location or external site according to the agreed specifications for the security personnel, including communication equipment if the site is external.\"\n        },\n        {\n          \"Number\": 4,\n          \"Ar\": \"يلتزم الطرف الثاني بعدم القيام بأحداث فتحات أو ثغرات جديدة لموقع الحراسة أو إضافة تشييد مباني جديدة إلا بعد إخطار الطرف الأول بذلك حتى يتمكن من ملائمة تعديل نقاط الحراسة بما يتماشى مع التغييرات الجديدة.\",\n          \"En\": \"The second party shall not create new openings or breaches at the security site or add new buildings without notifying the first party, allowing adjustments to be made to the security points to align with the new changes.\"\n        },\n        {\n          \"Number\": 5,\n          \"Ar\": \"الطرف الأول غير مسؤول عن أي أضرار تلحق بالطرف الثاني أو موظفيه أو ممتلكاته وذلك بسبب تكليف رجل الأمن بأي أعمال أخرى غير المناط به.\",\n          \"En\": \"The first party is not responsible for any damages incurred by the second party, its employees, or its property resulting from assigning security personnel tasks outside their designated duties.\"\n        },\n        {\n          \"Number\": 6,\n          \"Ar\": \"الطرف الأول غير مسؤول عن أي أضرار أو خسائر تلحق بالعميل أو موظفيه نتيجة عدم اتباع إجراءات الأمن والسلامة أو عدم توفير متطلبات الأمن والسلامة من أجهزة ومعدات مثل أجهزة الإنذار المبكر ومكافحة الحريق ومخارج الطوارئ وغير ذلك في مواقع الحراسة حسب تعليمات الدفاع المدني الخاصة بذلك.\",\n          \"En\": \"The first party is not liable for any damages or losses to the client or its employees resulting from non-compliance with security and safety procedures or failing to provide safety requirements, such as early warning systems, fire extinguishing equipment, emergency exits, etc., at the security sites, in accordance with Civil Defense regulations.\"\n        },\n        {\n          \"Number\": 7,\n          \"Ar\": \"يلتزم الطرف الثاني بتسديد فاتورة الخدمة الشهرية في نهاية كل شهر ميلادي تمت الخدمة الفعلية فيه بعد مراجعتها وتدقيقها دون تأخير حتى لا تؤثر على سير العمل وأداء الخدمة بحيث لا تتعدى خمسة أيام من تاريخ استلام الفاتورة الشهرية , على أن يقوم الطرف الأول برفع الفاتورة في الخامس والعشرين من كل شهر ميلادي.\",\n          \"En\": \"The second party shall pay the monthly service invoice at the end of each Gregorian month after reviewing and verifying it without delay, ensuring it does not affect workflow and service performance. Payment must be made within five days from receiving the invoice, which the first party will issue on the 25th of each month.\"\n        }\n      ]\n    },\n\n    {\n      \"Number\": 14,\n      \"Title\": {\n        \"Ar\": \"البند الرابع عشر\",\n        \"En\": \"Clause Fourteen\"\n      },\n      \"SubClauses\": [\n        {\n          \"Number\": 1,\n          \"Ar\": \"(في حال تأخر رجل الأمن أكثر من ساعة يتم خصم (ساعات التأخير.\",\n          \"En\": \"In case the security personnel is late by more than one hour, the delay hours will be deducted.\"\n        },\n        {\n          \"Number\": 2,\n          \"Ar\": \"في حال غياب رجل الأمن أو مشرف الوردية وعدم إحضار الدعم البديل كامل وقت استلام الوردية يتم خصم (اليوم).\",\n          \"En\": \"If the security personnel or shift supervisor is absent and no replacement is provided for the entire shift time, one full day will be deducted.\"\n        }\n      ]\n    },\n    {\n      \"Number\": 15,\n      \"Title\": {\n        \"Ar\": \"البند الخامس عشر\",\n        \"En\": \"Clause Fifteen\"\n      },\n      \"Ar\": \"في حالة تغيب رجل الأمن عن الحضور إلى موقع عمله يحق للطرف الثاني طلب استبداله خلال (48) ساعة من تاريخ إخطار الطرف الأول كتابياً.\",\n      \"En\": \"In the event that security personnel is absent from their workplace, the second party has the right to request a replacement within (48) hours from the date of written notification to the first party.\"\n    },\n    {\n      \"Number\": 16,\n      \"Title\": {\n        \"Ar\": \"البند السادس عشر\",\n        \"En\": \"Clause Sixteen\"\n      },\n      \"Ar\": \"يقوم الطرف الأول بتزويد الطرف الثاني بجميع أرقام هواتف الطوارئ لاستخدامها في حالة الغياب أو لأي سبب أو ملاحظات يراها الطرف الثاني.\",\n      \"En\": \"The first party shall provide the second party with all emergency contact numbers for use in cases of absence or for any reason or feedback deemed necessary by the second party.\"\n    },\n    {\n      \"Number\": 17,\n      \"Title\": {\n        \"Ar\": \"البند السابع عشر\",\n        \"En\": \"Clause Seventeen\"\n      },\n      \"Ar\": \"يلتزم الطرف الثاني بإخطار الطرف الأول كتابياً عن أي حدث يحصل خلال فترة أقصاها الـ ( 24 ساعة) التالية للحدث ، ويبلغ الطرف الثاني الطرف الأول خطياً عن أي مخالفة أو أخـــطاء يرتكبها رجل الأمن أثناء عمله وذلك خـــــلال الـ ( 24 ) ساعه من تاريخ علم الطرف الثاني بهذه المخالفة وعند وصول الإخطار للطرف الاول عليه القيام باتخاذ الإجراءات المناسبة فوراً لتصحيح موضوع المخالفة أو الخطأ.\",\n      \"En\": \"The second party is obligated to notify the first party in writing of any incident within (24) hours of its occurrence. Additionally, the second party must inform the first party in writing of any violations or mistakes made by the security personnel during their duties within (24) hours of becoming aware of the issue. Upon receiving the notification, the first party must take immediate corrective action to resolve the violation or error.\"\n    },\n    {\n      \"Number\": 18,\n      \"Title\": {\n        \"Ar\": \"البند الثامن عشر\",\n        \"En\": \"Clause Eighteen\"\n      },\n      \"Ar\": \"يحق للطرف الثاني طلب رجال أمن اضافيين وذلك حسب حاجة عمله ، وبخطاب خطي يشار فيه للعقد المبرم ويحدد قيمة رجل الأمن الواحد ، أما إذا كانت حاجة الطلب مستمرة لأكثر من شهر فإنه يتم عمل ملحق للعقد متضمن الأعداد المطلوبة.\",\n      \"En\": \"The second party has the right to request additional security personnel based on its operational needs through a written request referencing the signed contract and specifying the cost per security personnel. If the requirement persists for more than one month, an annex to the contract shall be made including the required numbers.\"\n    },\n    {\n      \"Number\": 19,\n      \"Title\": {\n        \"Ar\": \"البند التاسع عشر\",\n        \"En\": \"Clause Nineteen\"\n      },\n      \"Ar\": \"يلتزم الطرف الثاني بتركيب كاميرات مراقبه في المنشأة حسب تعميم وزارة الداخلية قرار 4044 وتاريخ 25/05/1440 وعدم التركيب للكاميرات يعد مخالفة يتعرض بسببها للغرامات والجزائيات.\",\n      \"En\": \"The second party is obligated to install surveillance cameras at the facility in accordance with the Ministry of Interior's directive No. 4044 dated 25/05/1440. Failure to install the cameras constitutes a violation subject to fines and penalties.\"\n    },\n    {\n      \"Number\": 20,\n      \"Title\": {\n        \"Ar\": \"البند العشرون\",\n        \"En\": \"Clause Twenty\"\n      },\n      \"Ar\": \"في حال وجود خلاف أو نزاع بين الطرفين في تفسير هذه العقد فيتم حله بالطرق الودية وإن لم يتوصل الطرفين إلى حل لهذا النزاع ، فيتم اللجوء إلى الجهات الرسمية للفصل فيه.\",\n      \"En\": \"In the event of a disagreement or dispute between the parties regarding the interpretation of this contract, the issue shall be resolved amicably. If the parties fail to reach a resolution, the matter shall be referred to the official authorities for a decision.\"\n    },\n    {\n      \"Number\": 21,\n      \"Title\": {\n        \"Ar\": \"البند الحادي والعشرون\",\n        \"En\": \"Clause Twenty-One\"\n      },\n      \"Ar\": \"يتعهد كل من الطرفين بالمحافظة على سرية العقد وما ينتج عنها من أنشطة أو تعليمات.\",\n      \"En\": \"Both parties are committed to maintaining the confidentiality of the contract and any activities or instructions resulting from it.\"\n    },\n    {\n      \"Number\": 22,\n      \"Title\": {\n        \"Ar\": \"البند الثاني والعشرون\",\n        \"En\": \"Clause Twenty-Two\"\n      },\n      \"Ar\": \"يخضع هذا العقد في تفسيره وتنفيذه للوائح والأنظمة السارية المفعول بالمملكة العربية السعودية.\",\n      \"En\": \"This contract shall be interpreted and enforced in accordance with the laws and regulations in force in the Kingdom of Saudi Arabia.\"\n    },\n    {\n      \"Number\": 23,\n      \"Title\": {\n        \"Ar\": \"البند الثالث والعشرون\",\n        \"En\": \"Clause Twenty-Three\"\n      },\n      \"Ar\": \"تم التوقيع على هذه الاتفاقية من نسختين ست صفحات لكل نسخة ، يسلم كل طرف نسخة أصلية للعمل بموجبها.\",\n      \"En\": \"This agreement has been signed in two copies, with six pages for each copy. Each party shall receive an original copy to act accordingly.\"\n    }\n  ],\n  \"ClosingRemark\": {\n    \"Ar\": \"والله ولي التوفيق\",\n    \"En\": \"Allah is the source of success\"\n  }\n}\n" });

            migrationBuilder.CreateIndex(
                name: "IX_ContractTemplate_OfferId",
                table: "ContractTemplate",
                column: "OfferId",
                unique: true);
        }
    }
}
