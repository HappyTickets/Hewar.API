using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Staticcontractdataseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StaticClauses",
                columns: new[] { "Id", "ContentAr", "ContentEn", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "ModifiedBy", "ModifiedOn", "TenantId", "TitleAr", "TitleEn" },
                values: new object[,]
                {
                    { 1L, "يعتبر التمهيد السابق جزءاً لا يتجزأ من هذا العقد ومكملاً ومفسراً له وكذلك أي ملاحق يتفق عليها الطرفان.", "The previous preamble is considered an integral part of this contract and complements and interprets it, as well as any appendices agreed upon by both parties.", null, null, null, null, false, null, null, null, "البند الأول", "Clause One" },
                    { 2L, "إن الغرض من العقد هو قيام الطرف الاول بتأمين وتقديم خدمة الحراسة الأمنية المدنية الخاصة للطرف الثاني.", "The purpose of this contract is for the first party to provide and ensure private civil security services for the second party.", null, null, null, null, false, null, null, null, "البند الثاني", "Clause Two" },
                    { 3L, "يعتبر عرض السعر المقدم من الطرف الأول من وثائق العقد الأساسية لما فيها من بيان للخدمات التي يقدمها و أسعارها , كما يعتبر كل تعميد مقدم من الطرف الثاني إلى الطرف الأول خلال مدة العقد من وثائق العقد الأساسية لما فيها من بيان لأعداد أفراد الأمن ومواقع الخدمة.", "The price quotation submitted by the first party is considered a fundamental document of the contract as it details the services offered and their prices. Additionally, any authorization given by the second party to the first party during the contract period is regarded as a core document detailing the number of security personnel and service locations.", null, null, null, null, false, null, null, null, "البند الثالث", "Clause Three" },
                    { 4L, "مدة العقد سنة ميلادية (من عرض السعر) قابلة للتجديد لمدة أو مدد مماثلة، ويحق لأي طرف فسخ أو إلغاء هذا العقد شرط إشعار الطرف الأخر خطياً برغبته قبل (60) ستون يوماً من تاريخ الفسخ أو الإلغاء الفعلي للعقد مع دفع جميع المستحقات حتى نهاية الإشعار.", "The contract duration is one Gregorian year (from the date of the price quotation) and is renewable for the same period or periods. Either party has the right to terminate or cancel this contract by notifying the other party in writing at least sixty (60) days prior to the actual termination or cancellation date, with all dues being settled up to the end of the notification period.", null, null, null, null, false, null, null, null, "البند الرابع", "Clause Four" },
                    { 5L, "إن مؤسسة {{CompanyNameAr}} تؤمن خدماتها طبقاً للأنظمة المعمول بها في المملكة العربية السعودية وفي حالة صدور أنظمة أو تعليمات جديدة تتعلق بالخدمات موضوع العقد مباشرة أو غير مباشرة تحتفظ {{CompanyNameAr}}  بالحق في تعديل قيمة العقد وفقاً للأنظمة والتعليمات الجديدة وذلك بعد الرجوع إلى الطرف الثاني. وخاصه في أنظمة وزارة الموارد البشرية الخاصة بــ مكتب العمل وتحديثات أنظمة ساعات العمل.", "{{CompanyNameEn}} provides its services in accordance with the regulations applicable in the Kingdom of Saudi Arabia. In case of issuance of new regulations or instructions directly or indirectly related to the services under the contract, {{CompanyNameEn}} reserves the right to adjust the contract value in accordance with the new regulations and instructions after consulting with the second party, particularly regarding the regulations of the Ministry of Human Resources related to the Labor Office and updates to the working hours systems.", null, null, null, null, false, null, null, null, "البند الخامس", "Clause Five" },
                    { 6L, "يبدأ العقد اعتباراً من تاريخ مباشرة العمل بتاريخ {{ContractStartDate}}.", "The contract begins from the commencement date of work on {{ContractStartDate}}.", null, null, null, null, false, null, null, null, "البند السادس", "Clause Six" },
                    { 7L, "1. حرر هذا العقد وفقاً لما جاء بأمر مقام وزير الداخلية (الأمن العام والقاضي بسعودة الحراسات الأمنية) وذلك بإحلال السعوديين بدلاً من رجال الأمن الغير سعوديين، حيث أننا مؤسسة تقدم الكوادر السعودية مائة بالمائة ومدربة كل في تخصصه.\n2. هذا العقد يلغي ما قبله من العقود إن وجدت.\n3. يلتزم الطرف الاول بتأمين الخدمات الأمنية الخاصة للطرف الثاني في موقعه {{PartyTwo.LocationToBeSecuredAr}} بواقع فقط عدد ({{CompanyGuardsCount}}) رجال أمن لا غير، على مستوى من الكفاءة والخبرة في المجال الأمني.\n4. يلتزم الطرف الأول بتأمين الخدمات الأمنية الخاصة على مدار ساعات العمل التي يحددها الطرف الثاني على أن لا تتجاوز عدد الساعات عن ثمان ساعات عمل لكل رجل أمن واحد.", "1. This contract is issued in accordance with the directive of the Minister of Interior (Public Security), which mandates the Saudization of security services by replacing non-Saudi security personnel with Saudi nationals. Our institution provides 100% Saudi personnel who are professionally trained in their respective specializations.\n2. This contract supersedes any previous contracts, if they exist.\n3. The first party is committed to providing private security services to the second party at their location {{PartyTwo.LocationToBeSecuredEn}}, with a total of ({{CompanyGuardsCount}}) security guards only, possessing high efficiency and experience in the security field.\n4. The first party is committed to providing private security services during the working hours specified by the second party, ensuring that the working hours do not exceed eight hours per security guard.", null, null, null, null, false, null, null, null, "البند السابع", "Clause Seven" },
                    { 8L, "تقوم {{CompanyNameAr}} بتعيين رجال أمن على مستوى مميز من التدريب وحيث أن المجموعة لديها مراكز للتدريب والتأهيل والتطوير كل حسب اختصاصه للقيام بتوفير الخدمات الأمنية الخاصة للمواقع وفقاً للشروط والتعليمات الواردة من وزارة الداخلية والتوجيهات الصادرة من الجهات الرسمية فيما يتعلق بالخدمات الأمنية وعلى الأخص أن يكون حسن السيرة والسلوك واللياقة الطبية والبدنية والقدرة على القيام بجميع الإجراءات والأعمال الأمنية الخاصة والدوريات على أعلى مستوى.", "{{CompanyNameEn}} appoints security personnel with exceptional levels of training. The group has specialized training, qualification, and development centers to provide specialized security services for sites in accordance with the conditions and instructions issued by the Ministry of Interior and official authorities regarding security services. In particular, security personnel must possess good conduct and behavior, medical and physical fitness, and the ability to perform all special security procedures and patrol duties at the highest level.", null, null, null, null, false, null, null, null, "البند الثامن : صفات رجال الامن", "Clause Eight: Security Personnel Qualifications" },
                    { 9L, "على الطرف الاول تقديم خدمة الإشراف والمتابعة والرقابة لعناصره في الموقع من خلال الاتصالات الدورية والزيارات المفاجئة للتأكد من تواجدهم وحسن أدائهم للعمل.", "The first party shall provide supervision, follow-up, and monitoring services for its personnel on-site through regular communication and unannounced visits to ensure their presence and the quality of their work performance.", null, null, null, null, false, null, null, null, "البند التاسع", "Clause Nine" },
                    { 10L, "يقوم رجال الأمن بإتباع التعليمات الواردة من مسئولي الطرف الثاني بالتنسيق مع الطرف الاول في هذا الخصوص وتتمثل مهام رجل الأمن في حراسة الموقع ومنع دخول الأشخاص الغير مصرح لهم وإبلاغ الجهات المسئولة في الحالات التي تستدعي تبليغ الجهات الرسمية كالدفاع المدني أو الشرطة.", "Security personnel shall follow the instructions provided by the representatives of the second party in coordination with the first party. Their duties include guarding the site, preventing unauthorized access, and notifying the relevant authorities in situations requiring official notification, such as emergencies involving civil defense or police.", null, null, null, null, false, null, null, null, "البند العاشر", "Clause Ten" },
                    { 11L, "يلتزم الطرف الأول بتأمين وتوفير رجال الأمن للموقع المشار إليه في المقدمة يتم احتساب عدد ساعات الوردية بواقع (8) ساعات يوميا , (6) ستة أيام في الأسبوع باستثناء شهر رمضان المبارك حيث يتم احتساب عدد ساعات الوردية بما لا يزيد عن ستة ساعات في اليوم ويتم احتساب الساعتين الإضافيتين كعمل إضافي ويتم توضيحه في فاتورة الخدمة.", "The first party is committed to providing security personnel for the site mentioned in the preamble. The shift hours are calculated as (8) hours per day, (6) days per week, except during the holy month of Ramadan, where shift hours do not exceed six hours per day. Any additional two hours will be considered overtime and will be detailed in the service invoice.", null, null, null, null, false, null, null, null, "البند الحادي عشر", "Clause Eleven" },
                    { 13L, "1. يقوم الطرف الثاني بالتأمين على ممتلكاته ضد الدخول للموقع للغير المصرح لهم بالدخول او ليس من منسوبي الشركة او الموقع المحدد او السطو علي الموقع  والتبليغ للجهات المختصة في حال نشوب حرائق.\n\n2. لا يحق التعاقد مع أي موظف تابع لشركة  {{CompanyNameAr}} أو توظيفه إلا بعد انقضاء ستة أشهر من تركه الخدمة بالمؤسسة، وفي حال الإخلال بهذا البند يجب دفع ثلاث رواتب عن كل موظف تم تعينه بهذه الطريقة.\n\n3. يلتزم الطرف الثاني بتأمين موقع أو مقر خارجي حسب المواصفات المتفق عليها لرجل الأمن ووسيلة اتصال إذا كان الموقع خارجي.\n\n4. يلتزم الطرف الثاني بعدم القيام بأحداث فتحات أو ثغرات جديدة لموقع الحراسة أو إضافة تشييد مباني جديدة إلا بعد إخطار الطرف الأول بذلك حتى يتمكن من ملائمة تعديل نقاط الحراسة بما يتماشى مع التغييرات الجديدة.\n\n5. الطرف الأول غير مسؤول عن أي أضرار تلحق بالطرف الثاني أو موظفيه أو ممتلكاته وذلك بسبب تكليف رجل الأمن بأي أعمال أخرى غير المناط به.\n\n6.  الطرف الأول غير مسؤول عن أي أضرار أو خسائر تلحق بالعميل أو موظفيه نتيجة عدم اتباع إجراءات الأمن والسلامة أو عدم توفير متطلبات الأمن والسلامة من أجهزة ومعدات مثل أجهزة الإنذار المبكر ومكافحة الحريق ومخارج الطوارئ وغير ذلك في مواقع الحراسة حسب تعليمات الدفاع المدني الخاصة بذلك.\n\n7. يلتزم الطرف الثاني بتسديد فاتورة الخدمة الشهرية في نهاية كل شهر ميلادي تمت الخدمة الفعلية فيه بعد مراجعتها وتدقيقها دون تأخير حتى لا تؤثر على سير العمل وأداء الخدمة بحيث لا تتعدى خمسة أيام من تاريخ استلام الفاتورة الشهرية , على أن يقوم الطرف الأول برفع الفاتورة في الخامس والعشرين من كل شهر ميلادي.", "1. The second party is responsible for insuring its property against unauthorized access or entry by non-employees of the company or specified site and against burglary. The second party must also notify the relevant authorities in case of fire incidents.\n\n2. No employee of {{CompanyNameEn}} may be hired or contracted by the second party until six months have passed since the employee left the company. If this clause is violated, three months' salaries must be paid for each employee hired in this manner.\n\n3. The second party is obligated to provide a location or external site according to the agreed specifications for the security personnel, including communication equipment if the site is external.\n\n4. The second party shall not create new openings or breaches at the security site or add new buildings without notifying the first party, allowing adjustments to be made to the security points to align with the new changes.\n\n5. The first party is not responsible for any damages incurred by the second party, its employees, or its property resulting from assigning security personnel tasks outside their designated duties.\n\n6. The first party is not liable for any damages or losses to the client or its employees resulting from non-compliance with security and safety procedures or failing to provide safety requirements, such as early warning systems, fire extinguishing equipment, emergency exits, etc., at the security sites, in accordance with Civil Defense regulations.\n\n7. The second party shall pay the monthly service invoice at the end of each Gregorian month after reviewing and verifying it without delay, ensuring it does not affect workflow and service performance. Payment must be made within five days from receiving the invoice, which the first party will issue on the 25th of each month.", null, null, null, null, false, null, null, null, "البند الثالث عشر", "Clause Thirteen" },
                    { 14L, "1. (في حال تأخر رجل الأمن أكثر من ساعة يتم خصم (ساعات التأخير.\n\n2. في حال غياب رجل الأمن أو مشرف الوردية وعدم إحضار الدعم البديل كامل وقت استلام الوردية يتم خصم (اليوم).", "1. In case the security personnel is late by more than one hour, the delay hours will be deducted.\n\n2. If the security personnel or shift supervisor is absent and no replacement is provided for the entire shift time, one full day will be deducted.", null, null, null, null, false, null, null, null, "البند الرابع عشر", "Clause Fourteen" },
                    { 15L, "في حالة تغيب رجل الأمن عن الحضور إلى موقع عمله يحق للطرف الثاني طلب استبداله خلال (48) ساعة من تاريخ إخطار الطرف الأول كتابياً.", "In the event that security personnel is absent from their workplace, the second party has the right to request a replacement within (48) hours from the date of written notification to the first party.", null, null, null, null, false, null, null, null, "البند الخامس عشر", "Clause Fifteen" },
                    { 16L, "يقوم الطرف الأول بتزويد الطرف الثاني بجميع أرقام هواتف الطوارئ لاستخدامها في حالة الغياب أو لأي سبب أو ملاحظات يراها الطرف الثاني.", "The first party shall provide the second party with all emergency contact numbers for use in cases of absence or for any reason or feedback deemed necessary by the second party.", null, null, null, null, false, null, null, null, "البند السادس عشر", "Clause Sixteen" },
                    { 17L, "يلتزم الطرف الثاني بإخطار الطرف الأول كتابياً عن أي حدث يحصل خلال فترة أقصاها الـ ( 24 ساعة) التالية للحدث ، ويبلغ الطرف الثاني الطرف الأول خطياً عن أي مخالفة أو أخـــطاء يرتكبها رجل الأمن أثناء عمله وذلك خـــــلال الـ ( 24 ) ساعه من تاريخ علم الطرف الثاني بهذه المخالفة وعند وصول الإخطار للطرف الاول عليه القيام باتخاذ الإجراءات المناسبة فوراً لتصحيح موضوع المخالفة أو الخطأ.", "The second party is obligated to notify the first party in writing of any incident within (24) hours of its occurrence. Additionally, the second party must inform the first party in writing of any violations or mistakes made by the security personnel during their duties within (24) hours of becoming aware of the issue. Upon receiving the notification, the first party must take immediate corrective action to resolve the violation or error.", null, null, null, null, false, null, null, null, "البند السابع عشر", "Clause Seventeen" },
                    { 18L, "يحق للطرف الثاني طلب رجال أمن اضافيين وذلك حسب حاجة عمله ، وبخطاب خطي يشار فيه للعقد المبرم ويحدد قيمة رجل الأمن الواحد ، أما إذا كانت حاجة الطلب مستمرة لأكثر من شهر فإنه يتم عمل ملحق للعقد متضمن الأعداد المطلوبة.", "The second party has the right to request additional security personnel based on its operational needs through a written request referencing the signed contract and specifying the cost per security personnel. If the requirement persists for more than one month, an annex to the contract shall be made including the required numbers.", null, null, null, null, false, null, null, null, "البند الثامن عشر", "Clause Eighteen" },
                    { 19L, "يلتزم الطرف الثاني بتركيب كاميرات مراقبه في المنشأة حسب تعميم وزارة الداخلية قرار 4044 وتاريخ 25/05/1440 وعدم التركيب للكاميرات يعد مخالفة يتعرض بسببها للغرامات والجزائيات.", "The second party is obligated to install surveillance cameras at the facility in accordance with the Ministry of Interior's directive No. 4044 dated 25/05/1440. Failure to install the cameras constitutes a violation subject to fines and penalties.", null, null, null, null, false, null, null, null, "البند التاسع عشر", "Clause Nineteen" },
                    { 20L, "في حال وجود خلاف أو نزاع بين الطرفين في تفسير هذه العقد فيتم حله بالطرق الودية وإن لم يتوصل الطرفين إلى حل لهذا النزاع ، فيتم اللجوء إلى الجهات الرسمية للفصل فيه.", "In the event of a disagreement or dispute between the parties regarding the interpretation of this contract, the issue shall be resolved amicably. If the parties fail to reach a resolution, the matter shall be referred to the official authorities for a decision.", null, null, null, null, false, null, null, null, "البند العشرون", "Clause Twenty" },
                    { 21L, "يتعهد كل من الطرفين بالمحافظة على سرية العقد وما ينتج عنها من أنشطة أو تعليمات.", "Both parties are committed to maintaining the confidentiality of the contract and any activities or instructions resulting from it.", null, null, null, null, false, null, null, null, "البند الحادي والعشرون", "Clause Twenty-One" },
                    { 22L, "يخضع هذا العقد في تفسيره وتنفيذه للوائح والأنظمة السارية المفعول بالمملكة العربية السعودية.", "This contract shall be interpreted and enforced in accordance with the laws and regulations in force in the Kingdom of Saudi Arabia.", null, null, null, null, false, null, null, null, "البند الثاني والعشرون", "Clause Twenty-Two" },
                    { 23L, "تم التوقيع على هذه الاتفاقية من نسختين ست صفحات لكل نسخة ، يسلم كل طرف نسخة أصلية للعمل بموجبها.", "This agreement has been signed in two copies, with six pages for each copy. Each party shall receive an original copy to act accordingly.", null, null, null, null, false, null, null, null, "البند الثالث والعشرون", "Clause Twenty-Three" },
                    { 24L, "• المعرفة التامة بإجراءات الإيقاف وتبليغ السلطات المختصة عند الضرورة والتحفظ على المشتبه فيهم داخل حدود الموقع.\n• تنظيم حركة دخول وخروج رواد الموقع وضبط ورقابة المداخل والمخارج.\n• المعرفة التامة بكيفية التعامل مع المعدات الأمنية المتاحة وأمثل الطرق لاستخدامها.\n• استيعاب تعليمات الأمن الدائمة والالتزام بتنفيذها.\n• إجادة أسلوب التعامل مع الآخرين في إطار العلاقات العامة والاستقبال.\n• المحافظة على مواعيد العمل حضوراً وانصرافاً مع ضرورة التواجد على رأس العمل أثناء ساعات الدوام الرسمية.\n• الالتزام بارتداء الزي الرسمي الخاص برجال الأمن وعليه شعار الطرف الأول في جميع أوقات العمل والمحافظة على نظافة وحسن المظهر العام.\n• حمل المعدات الشخصية والضرورية التي تطلبها ضرورة العمل.\n\n• القيام بواجبات الحراسة الأمنية الراجلة والراكبة وكافة ما يتطلب عمل الحارس الأمني من واجب المراقبة والمتابعة والانتباه للمواقع التي يحددها الطرف الثاني على مدار عمله.\n• تقديم المساعدة والإسعافات الأولية والإرشادات لرواد الموقع وتوجيههم في حالة طلبهم ذلك وتنظيم عملية الدخول والخروج في الأماكن الغير مسموح بدخولها وإغلاق الأبواب ومنع الدخول عند انتهاء المواعيد المحددة من قبل الطرف الثاني وتشغيل وإطفاء الإنارة وقت الحاجة.\n• تولي المسئولية لدى حدوث أي مشاجرات أو مشاكل داخل الموقع والعمل على منع تطورها أو حلها أو رفعها لممثل الطرف الثاني.\n• اتخاذ كافة الاحتياطات وإجراءات السلامة اللازمة والتوجه بها للطرف الثاني لدرء الأخطار المحدقة مثل (إبعاد أي مواد أو أجهزة مشتبه فيها أو قد ينشأ من وجودها خطر حريق أو انفجار).\n• تدقيق وفحص التصاريح والوثائق والمستندات الخاصة بالتحكم في دخول المبنى ومراقبة سلامته وإنارته مثل (شاشات المراقبة – أجهزة الإنذار – الأبواب – أجهزة الاتصال – الإضاءة).\n• اتخاذ ما يلزم عند حدوث أي تحرك أو نشاط مشبوه أو أية مشاكل أو أحداث غير طبيعية داخل أو حول المواقع من قبل أي شخص أو مجموعات والإبلاغ الفوري عنها.\n• يلتزم الطرف الأول بالتأكد من قيام حراس الأمن التابعين له بمهام الحراسة المطلوبة منهم على مدار الساعة.\n• يلتزم حراس الأمن بعدم الإفشاء عن أي معلومات أو وقائع تصل إليهم أثناء تواجدهم في الموقع لأي جهة أخرى.\n• يلتزم الطرف الأول بتحمل كافة الالتزامات التي تنشأ عند تشغيله لحراس الأمن ويلتزم بدفع رواتبهم في مواعيدها حيث لن يقبل الطرف الثاني أية مطالبة مباشرة منهم ويتحمل الطرف الأول كافة الالتزامات المالية المتعلقة بهم ويستوفي كافة الإجراءات اللازمة لتشغيلهم.\n• لا يحق للطرف الثاني إعطاء موظفي الطرف الأول رواتبهم أو سلفه مالية وإذا قام بذلك لا يحق له خصمها من مستحقات الطرف الأول .\n• يقوم الطرف الأول بتغطية المواقع في حالة العطلات والأعياد والجمع والإجازات.", "• Full knowledge of stop procedures and reporting to the relevant authorities when necessary, and detaining suspects within the site boundaries.\n• Organizing the movement of site visitors and controlling the entrances and exits.\n• Full knowledge of how to handle available security equipment and the best methods for its use.\n• Understanding and adhering to ongoing security instructions.\n• Proficiency in dealing with others in public relations and reception.\n• Maintaining work schedules, both in attendance and departure, with a requirement to be on duty during official working hours.\n• Complying with wearing the official uniform with the first party's logo at all times during work hours and maintaining cleanliness and a neat appearance.\n• Carrying personal and essential equipment required by the nature of the job.\n\n• Performing the duties of foot and mounted security guards, including surveillance, monitoring, and paying attention to the locations specified by the second party during their shift.\n• Providing assistance, first aid, guidance to site visitors, and directing them when requested. Regulating entry and exit to restricted areas, locking doors, and preventing access after the designated times by the second party, including turning lights on or off when necessary.\n• Taking responsibility in the event of any disputes or issues within the site, working to prevent escalation, resolve the issue, or report it to the second party’s representative.\n• Taking all necessary safety precautions and reporting to the second party to mitigate potential hazards, such as moving any suspicious materials or equipment that could pose a fire or explosion risk.\n• Inspecting and verifying permits, documents, and records related to building access control, safety, and lighting, such as surveillance monitors, alarms, doors, communication devices, and lighting.\n• Taking action in the event of any suspicious movement or activity or any unusual incidents or events within or around the site, reported immediately.\n• The first party must ensure that their security guards are fulfilling their required duties around the clock.\n• Security guards must not disclose any information or incidents they become aware of during their presence on the site to any other party.\n• The first party is responsible for all obligations related to employing security guards, including paying their salaries on time. The second party will not accept any direct claims from the guards and the first party assumes all financial obligations regarding them and completes all necessary employment procedures.\n• The second party does not have the right to pay the first party’s employees their salaries or offer them financial advances. If done, it cannot be deducted from the first party's dues.\n• The first party is responsible for covering the sites during holidays, public holidays, and vacation periods.", null, null, null, null, false, null, null, null, "الواجبات والخدمات", "Duties & Services" }
                });

            migrationBuilder.InsertData(
                table: "StaticContracts",
                columns: new[] { "Id", "ClosingRemarkAr", "ClosingRemarkEn", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsDeleted", "ModifiedBy", "ModifiedOn", "PreambleAr", "PreambleEn", "TenantId", "TitleAr", "TitleEn" },
                values: new object[] { 1L, "والله ولي التوفيق", "Allah is the source of success", null, null, null, null, false, null, null, "أبرم هذا العقد بين كل من: الطرف الأول: شركة {{CompanyNameAr}} ومركزها الرئيسي في مدينة {{CompanyMainOfficeCityAr}} مسجــلة بالسجل التجاري رقم ({{CompanyCommercialRegistration}}) ترخيص الأمن العام رقم {{CompanyPublicSecurityLicense}} هاتف {{CompanyTelephone}} جوال {{CompanyMobile}} العنوان الوطني {{CompanyAddressCityAr}} الرمز البريدي ({{CompanyAddressPostalCode}}) وحدة رقم ({{CompanyAddressUnitNumber}}) مبنى رقم ({{CompanyAddressBuildingNumber}}) رقم التسجيل في سبل، واصل ({{CompanyRegistrationInSabl}}) بريد إلكتروني {{CompanyEmail}} ويمثلها في التوقيع على هذا العقد  / {{CompanyRepresentativeNameAr}} بصفته {{CompanyRepresentativeTitleAr}}. الطرف الثاني: السادة/ {{FacilityNameAr}} العقد الأساسي الموقع بتاريخ {{ContractSignDate}} ومركزها الرئيسي في مدينة {{FacilityMainOfficeCityAr}} : مسجــلة بالسجل التجاري مسجل بمدينة {{FacilityCommercialRegistrationCityAr}}  جوال ({{FacilityMobile}}) العنوان الوطني ({{FacilityAddressCityAr}}) الرمز البريدي ({{FacilityAddressPostalCode}}) وحدة رقم ({{FacilityAddressUnitNumber}}) مبنى رقم ({{FacilityAddressBuildingNumber}}) بريد إلكتروني {{FacilityEmail}} ويمثلها في التوقيع على هذا العقد الأستاذ / {{FacilityRepresentativeNameAr}} بصفته :  {{FacilityRepresentativeTitleAr}}.                     حيث أن الطرف الثاني يرغب في تأمين خدمات الحراسة الأمنية المدنية لموقعه {{FacilityLocationToBeSecuredAr}} فتقدم الطرف الأول بعرضه رقـــــــم ({{OfferNumber}}) وتاريخ ({{OfferDate}}) المرفق به بيان الخدمات التي يقدمها الطرف الأول وأسلوبها وأسعارها وقد لقي العرض قبولاً لدى الطرف الثاني وعليه فقد اتفق الطرفان وتراضيا على البنود والشروط التالية: عدم فسخ العقد من قبل الطرفين الا بعد أشعار شعبة الحراسات الأمنية بالميدانية بذالك. الالتزام بعمل جدول موضح به عدد الحراسات وأوقات ساعات العمل والتقيد بالتعليمات فيما يخص نظام مكتب العمل والعمال من حيث عدد ساعات العمل بشهر رمضان المبارك.", "This contract is made between: First Party: Company {{CompanyNameEn}} with its headquarters in {{CompanyMainOfficeCityEn}}, registered under Commercial Registration No. ({{CompanyCommercialRegistration}}), Public Security License No. {{CompanyPublicSecurityLicense}}, Phone {{CompanyTelephone}}, Mobile {{CompanyMobile}}, National Address {{CompanyAddressCityEn}}, Postal Code ({{CompanyAddressPostalCode}}), Unit No. ({{CompanyAddressUnitNumber}}), Building No. ({{CompanyAddressBuildingNumber}}), registered in Wasel as ({{CompanyRegistrationInSabl}}), Email {{CompanyEmail}}. Represented by {{CompanyRepresentativeNameEn}} as {{CompanyRepresentativeTitleEn}}. Second Party: {{FacilityNameEn}} with the main contract signed on {{ContractSignDate}} and its headquarters in {{FacilityMainOfficeCityEn}}. Registered in the Commercial Registry in the city of {{FacilityCommercialRegistrationCityEn}}, Mobile ({{FacilityMobile}}), National Address ({{FacilityAddressCityEn}}), Postal Code ({{FacilityAddressPostalCode}}), Unit No. ({{FacilityAddressUnitNumber}}), Building No. ({{FacilityAddressBuildingNumber}}), Email {{FacilityEmail}}. Represented by {{FacilityRepresentativeNameEn}} as {{FacilityRepresentativeTitleEn}}.                                       Whereas the Second Party desires to secure security guard services for its location {{FacilityLocationToBeSecuredEn}}, the First Party submitted its offer No. ({{OfferNumber}}) dated ({{OfferDate}}), including the statement of services offered, their method, and pricing. The offer was accepted by the Second Party. Therefore, both parties have agreed to the following terms and conditions: The contract cannot be terminated by either party without notifying the Security Guards Division in the field. A schedule specifying the number of guards and working hours must be prepared, adhering to the Labor Office regulations regarding working hours during the holy month of Ramadan.", null, "عقد تقديم خدمات الحراسات الأمنية المدنية الخاصة", "Contract for the Provision of Private Civil Security Guard Services" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "StaticClauses",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "StaticContracts",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
