using Microsoft.EntityFrameworkCore;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;
using Qick.Repositories.Interfaces;
using System.Linq;

namespace Qick.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly QickDatabaseManangementContext _context;

        public TestRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Test>> GetListActiveTest()
        {
            var testMember = await _context.Tests
                .Where(u => u.Status == Status.ACTIVE)
                .Include(i => i.Creator)
                .Include(u => u.TestType)
                .ToListAsync();

            return testMember;
        }

        public async Task<IEnumerable<Test>> GetListActiveTestGuest()
        {
            var testMember = await _context.Tests
                  .Where(u => u.Status == Status.ACTIVE)
                  .ToListAsync();
            return testMember;
        }

        public async Task<Test> GetTestById(int testId)
        {
            var testDetail = await _context.Tests
                .Where(a => a.Id == testId)
                .Include(i => i.Creator)
                .Include(u => u.TestType)
                .FirstOrDefaultAsync();
            return testDetail;
        }

        public async Task<Test> GetTestToAttemp(int testId)
        {
            var testMember = await _context.Tests
                .Where(a => a.Id == testId)
                .Include(u => u.Questions.Where(a => a.Status == Status.ACTIVE))
                .ThenInclude(q => q.Options.Where(a => a.Status == Status.ACTIVE))
                .Where(a => a.Status == Status.ACTIVE)
                .FirstOrDefaultAsync();
            return testMember;
        }

       

        public async Task<Test> CreateTest(CreateTestRequest request, Guid userId)
        {
            try
            {
                Test test = new()
                {
                    CreatorId = userId,
                    TestName = request.QuizName,
                    TestTypeId = request.QuizTypeId,
                    TotalQuestion = 0,
                    CreatedDate = DateTime.Now,
                    Introduction =  request.Introduction,
                    History = request.History,
                    CriteriaInformation = request.CriteriaInformation,
                    BannerUrl = request.BannerUrl,
                    BackgroundUrl = request.BackgroundUrl, 
                    Status = Status.PENDING
                };
                await _context.Tests.AddAsync(test);
                await _context.SaveChangesAsync();
                return test;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Models.TestType>> GetActiveTestType()
        {
            var listTestType = await _context.TestTypes
                .Where(u => u.Status == Status.ACTIVE)
                .ToListAsync();

            return listTestType;
        }

        public async Task<IEnumerable<Test>> GetListAllTest(Guid userId)
        {
            var testMember = await _context.Tests
                .Include(u => u.TestType)
                .Include(i => i.Creator)
                .ToListAsync();

            return testMember;
        }

        public async Task<bool> CreateResult(ResultRequest request)
        {
            try
            {
                Character addResult = new()
                {
                   Id =Guid.NewGuid(),
                   TestId = request.TestId,
                   ResultName = request.ResultName,
                   ResultRelationship = request.ResultRelationship,
                   ResultSuccessRule   = request.ResultSuccessRule,
                   ResultSummary = request.ResultSummary,
                   ResultShortName = request.ResultShortName,
                   ResultPictureUrl = request.ResultPicture,
                   ResultCareer = request.ResultCareer,
                  Status = Status.ACTIVE
                };
                await _context.Characters.AddAsync(addResult);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Test> UpdateTestInformation(UpdateTestRequest test)
        {
            try
            {
                var testDb = await _context.Tests
                    .Where(u => u.Id == test.Id)
                    .FirstOrDefaultAsync();
                if (testDb != null)
                {
                    testDb.TestName = test.QuizName;
                    testDb.TotalQuestion = test.TotalQuestion;
                    testDb.Introduction = test.Introduction;
                    testDb.History = test.History;
                    testDb.CriteriaInformation = test.CriteriaInformation;
                    testDb.BannerUrl = test.BannerUrl;
                    testDb.BackgroundUrl = test.BackgroundUrl;
                    testDb.Status = test.Status;
                } 
                else
                {
                    { throw new Exception("Test does not exist"); }
                }

                await _context.SaveChangesAsync();
                return testDb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Attempt> CreateAttempt(int testId, Guid? userId, string result, string re1, string r2, string r3, string r4, string r5, string r6)
        {
            try
            {
                Attempt attempt = new()
                {
                    TestId = testId,
                    UserId = userId,
                    ResultShortName = result,
                    AttemptDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    Result1 = re1,
                    Result2 = r2,
                    Result3 = r3,
                    Result4 = r4,
                    Result5 = r5,
                    Result6 = r6
                };
                await _context.Attempts.AddAsync(attempt);
                await _context.SaveChangesAsync();
                return attempt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<bool> CreateAttemptDetail(int attempId ,int optionId, string selectedField)
        {
            try
            {
                    AttemptDetail addDetail = new()
                    {
                        AttemptId = attempId,
                        OptionId = optionId,
                        Status = Status.ACTIVE,
                        SelectedField = selectedField
                    };
                await _context.AttemptDetails.AddAsync(addDetail);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Attempt>> GetAttempt(Guid? userId)
        {
            var result = await _context.Attempts
                .Include(a => a.Test)
                .ThenInclude(u => u.Characters)
                .Where(u =>  u.UserId == userId)
                .OrderByDescending(x => x.AttemptDate)
                .ToListAsync();
            return result;
        }
        //public async Task<IEnumerable<Attempt>> GetAttemptForFilter(Guid? userId, int? testId)
        //{
        //    if (testId != null)
        //    {
        //        var result = await _context.Attempts
        //        .ThenInclude(u => u.Characters)
        //        .Where(u => u.UserId == userId)
        //        .OrderByDescending(x => x.AttemptDate)
        //        .ToListAsync();
        //        return result;
        //    }
        //    else
        //    {

        //    }

        //}
       
        public async Task<Character> GetCharacterResult(int testId, string? resultShortName)
        {
            try
            {
                var result = await _context.Characters
                             .Where(a => a.TestId == testId && a.ResultShortName.Equals(resultShortName))
                             .FirstOrDefaultAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<SubmitResponse> CalculateTestResult(CalculateResultRequest request, Guid? userId)
        {
            try
            {
                string typeResult = "";
                var type = await _context.Tests
                    .Where(i => i.Id == request.TestId)
                    .Include(u => u.TestType)
                    .FirstOrDefaultAsync();
                string?
                        result1 = null,
                        result2 = null,
                        result3 = null,
                        result4 = null,
                        result5 = null,
                        result6 = null;

                if (type.TestType.TestTypeName.Equals(TestTypeName.MBTI))
                {

                    double
                        isI = 0,
                        isE = 0,
                        isS = 0,
                        isN = 0,
                        isT = 0,
                        isF = 0,
                        isJ = 0,
                        isP = 0;
                    
                    foreach (var question in request.questions)
                    {
                            switch (question.Options.FirstOrDefault().optionValue)
                            {
                                case TypeMBTI.IsI:
                                    isI += 1;
                                    break;

                                case TypeMBTI.IsE:
                                    isE += 1;
                                    break;

                                case TypeMBTI.IsS:
                                    isS += 1;
                                    break;

                                case TypeMBTI.IsN:
                                    isN += 1;
                                    break;

                                case TypeMBTI.IsT:
                                    isT += 1;
                                    break;

                                case TypeMBTI.IsF:
                                    isF += 1;
                                    break;

                                case TypeMBTI.IsJ:
                                    isJ += 1;
                                    break;

                                case TypeMBTI.IsP:
                                    isP += 1;
                                    break;

                                default:
                                    { throw new Exception("Answer wrong"); }
                            }
                    }
                    if (isI > isE)
                    {
                        typeResult = "I";
                       double re1 = isI / (isI + isE) * 100.0;
                        result1 = "I-" + (int)re1 +"%";
                    } 
                    else
                    {
                        typeResult = "E";
                        double re1 = (isE / (isI + isE)) * 100.0;
                        result1 = "E-" + (int)re1 + "%";
                    }
                    if (isN > isS)
                    {
                        typeResult += "N";
                        double re2 = (isN / (isS + isN)) * 100.0;
                        result2 = "N-" + (int)re2 + "%";
                    }
                    else
                    {
                        typeResult += "S";
                        double re2 = (isS / (isS + isN)) * 100.0;
                        result2 = "S-" + (int)re2 + "%";
                    }
                    if (isF > isT)
                    {
                        typeResult += "F";
                        double re3 = (isF / (isF + isT)) * 100.0;
                        result3 = "F-" + (int)re3 + "%";
                    }
                    else
                    {
                        typeResult += "T";
                        double re3 = (isT / (isF + isT)) * 100.0;
                        result3 = "T-" + (int)re3+ "%";
                    }
                    if (isJ > isP)
                    {
                        typeResult += "J";
                        double re4 = (isJ / (isJ + isP)) * 100.0;
                        result4 = "J-" + (int)re4 + "%";
                    }
                    else
                    {
                        typeResult += "P";
                        double re4 = (isP / (isJ + isP)) * 100.0;
                        result4 = "P-" + (int)re4 + "%";
                    }

                }
                
                if (userId != null)
                {
                    var attempt = CreateAttempt(request.TestId, userId, typeResult, result1,result2,result3,result4,null,null);
                    foreach ( var question in request.questions)
                    {
                        foreach (var option in question.Options)
                        {
                                var detail = CreateAttemptDetail(attempt.Result.Id, option.optionId, option.optionValue);
                        }
                        
                    }
                }
                SubmitResponse response = new()
                {
                    Id = request.TestId,
                    ResultShortName = typeResult,
                    Result1 = result1,
                    Result2 = result2,
                    Result3 = result3,
                    Result4 = result4,
                    Result5 = result5,
                    Result6 = result6,
                };
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public async Task<SubmitResponse> CalculateHollandResult(CalculateResultRequest request, Guid? userId)
        {
            try
            {
                string typeResult = "Holland";
                string?
                        result1 = null,
                        result2 = null,
                        result3 = null,
                        result4 = null,
                        result5 = null,
                        result6 = null;
                double
                       isR = 0,
                       isI = 0,
                       isA = 0,
                       isS = 0,
                       isE = 0,
                       isC = 0;
                foreach (var question in request.questions)
                {
                    foreach (var option in question.Options)
                    {
                        switch (option.optionValue)
                        {
                            case TypeHolland.IsR:
                                isR += 1;
                                break;

                            case TypeHolland.IsI:
                                isI += 1;
                                break;

                            case TypeHolland.IsA:
                                isA += 1;
                                break;

                            case TypeHolland.IsS:
                                isS += 1;
                                break;

                            case TypeHolland.IsE:
                                isE += 1;
                                break;

                            case TypeHolland.IsC:
                                isC += 1;
                                break;
                            default:
                                { throw new Exception("Answer wrong"); }
                        }
                    }
                    
                }
                var countR = await _context.Options
                    .Where(a => a.Value.Equals(TypeHolland.IsR))
                    .CountAsync();
                var countI = await _context.Options
                    .Where(a => a.Value.Equals(TypeHolland.IsI))
                    .CountAsync();
                var countA = await _context.Options
                    .Where(a => a.Value.Equals(TypeHolland.IsA))
                    .CountAsync();
                var countS = await _context.Options
                    .Where(a => a.Value.Equals(TypeHolland.IsS))
                    .CountAsync();
                var countE = await _context.Options
                    .Where(a => a.Value.Equals(TypeHolland.IsE))
                    .CountAsync();
                var countC = await _context.Options
                    .Where(a => a.Value.Equals(TypeHolland.IsC))
                    .CountAsync();
                double re1 = (isR / Convert.ToDouble(countR)) * 100.0;
                result1 = "R-" + (int)re1 + "%";
                double re2 = (isI / Convert.ToDouble(countI)) * 100.0;
                result2 = "I-" + (int)re2 + "%";
                double re3 = (isA / Convert.ToDouble(countA)) * 100.0;
                result3 = "A-" + (int)re3 + "%";
                double re4 = (isS / Convert.ToDouble(countS)) * 100.0;
                result4 = "S-" + (int)re4 + "%";
                double re5 = (isE / Convert.ToDouble(countE)) * 100.0;
                result5 = "E-" + (int)re5 + "%";
                double re6 = (isC / Convert.ToDouble(countC)) * 100.0;
                result6 = "C-" + (int)re6 + "%";

              
                if (userId != null)
                {
                    var attempt = CreateAttempt(request.TestId, userId, typeResult, result1,result2,result3,result4,result5,result6);
                    foreach (var question in request.questions)
                    {
                        foreach (var option in question.Options)
                        {
                            var detail = CreateAttemptDetail(attempt.Result.Id, option.optionId, option.optionValue);

                        }

                    }
                    await _context.SaveChangesAsync();
                }
                SubmitResponse response = new()
                {
                    Result1 = result1,
                    Result2 = result2,
                    Result3 = result3,
                    Result4 = result4,
                    Result5 = result5,
                    Result6 = result6
                };
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<SubmitResponse> CalculateDiscResult(CalculateResultRequest request, Guid? userId)
        {
            try
            {
                string typeResult = "";
                int
                    yD = 0,
                    nD = 0,
                    yI = 0,
                    nI = 0,
                    yS = 0,
                    nS = 0,
                    yC = 0,
                    nC = 0;
                string?
                    result1 = null,
                    result2 = null,
                    result3 = null,
                    result4 = null;

                foreach (var question in request.questions)
                {
                    foreach (var option in question.Options)
                    {
                        if (option.selectedField == true )
                        {
                            switch (option.optionValue[0])
                            {
                                case 'O':
                                    yD += 1;
                                    break;
                                case 'A':
                                    yI += 1;
                                    break;
                                case 'B':
                                    yS += 1;
                                    break;
                                case 'P':
                                    yC += 1;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            switch (option.optionValue[2])
                            {
                                case 'O':
                                    nD += 1;
                                    break;
                                case 'A':
                                    nI += 1;
                                    break;
                                case 'B':
                                    nS += 1;
                                    break;
                                case 'P':
                                    nC += 1;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                int
                    isD = yD - nD,
                    isI = yI - nI,
                    isS = yS - nS,
                    isC = yC - nC;
                var D = await _context.IntensityIndices
                    .Where(a => a.Dimension == "D" && a.Value == isD)
                    .FirstOrDefaultAsync();
                var I = await _context.IntensityIndices
                    .Where(a => a.Dimension == "i" && a.Value == isI)
                    .FirstOrDefaultAsync();
                var S = await _context.IntensityIndices
                    .Where(a => a.Dimension == "S" && a.Value == isS)
                    .FirstOrDefaultAsync();
                var C = await _context.IntensityIndices
                    .Where(a => a.Dimension == "C" && a.Value == isC)
                    .FirstOrDefaultAsync();
                var list = checkGraphDisc(D,I,S,C).OrderByDescending(a => a.Percentage);

                result1 ="D-" + (int)D.Percentage +"%";
                result2 ="I-" + (int)I.Percentage +"%";
                result3 ="S-" + (int)S.Percentage +"%";
                result4 ="C-" + (int)C.Percentage +"%";

                switch (list.Count())
                {
                    case 1:
                        typeResult = list.ToList()[0].Dimension;  
                        break;
                    case 2:
                        typeResult = list.ToList()[0].Dimension + list.ToList()[1].Dimension;
                        break;
                    case >= 3:
                        if (list.ToList()[1].Segment > list.ToList()[2].Segment)
                        {
                            typeResult = list.ToList()[0].Dimension + list.ToList()[1].Dimension;
                        }
                        else
                        {
                            typeResult = list.ToList()[0].Dimension;
                        }
                        break;
                    default:
                        break;
                }

                switch (typeResult)
                {
                    case "DS":
                        typeResult = "D";
                        break;
                    case "SD":
                        typeResult = "S";
                        break;
                    case "IC":
                        typeResult = "I";
                        break;
                    case "CI":
                        typeResult = "C";
                        break;
                    default:
                        break;
                }
                if (userId != null)
                {
                    var attempt = CreateAttempt(request.TestId, userId, typeResult, result1,result2,result3,result4,null,null);
                    foreach (var question in request.questions)
                    {
                        foreach (var option in question.Options)
                        {
                            if (option.selectedField ==  true)
                            {
                                var detail = CreateAttemptDetail(attempt.Result.Id, option.optionId,"LIKE");
                            }
                            else
                            {
                                var detail = CreateAttemptDetail(attempt.Result.Id, option.optionId, "UNLIKE");
                            }
                            
                        }
                        
                    }
                    await _context.SaveChangesAsync();
                }
                SubmitResponse response = new()
                {
                   Id = request.TestId,
                   ResultShortName = typeResult,
                    Result1 = result1,
                    Result2 = result2,
                    Result3 = result3,
                    Result4 = result4
                };
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<SubmitResponse> CalculateBig5Result(CalculateResultRequest request, Guid? userId)
        {
            try
            {
                string typeResult = "BigFive";
                double
                    yO = 0,
                    nO = 0,
                    yC = 0,
                    nC = 0,
                    yE = 0,
                    nE = 0,
                    yA = 0,
                    nA = 0,
                    yN = 0,
                    nN = 0;
                string?
                    result1 = null,
                    result2 = null,
                    result3 = null,
                    result4 = null,
                    result5 = null;

                foreach (var question in request.questions)
                {
                    if (question.questionvalue[2].Equals("P"))
                    {
                        switch (question.questionvalue[0])
                        {
                            case 'O':
                                yO += Int32.Parse(question.Options.First().optionValue);
                                break;
                            case 'C':
                                yC += Int32.Parse(question.Options.First().optionValue);
                                break;
                            case 'E':
                                yE += Int32.Parse(question.Options.First().optionValue);
                                break;
                            case 'A':
                                yA += Int32.Parse(question.Options.First().optionValue);
                                break;
                            case 'N':
                                yN += Int32.Parse(question.Options.First().optionValue);
                                break;
                            default:
                                break;
                        }
                    }
                    else if (question.questionvalue[2].Equals("N"))
                    {
                        switch (question.questionvalue[0])
                        {
                            case 'O':
                                nO += Int32.Parse(question.Options.First().optionValue);
                                break;
                            case 'C':
                                nC += Int32.Parse(question.Options.First().optionValue);
                                break;
                            case 'E':
                                nE += Int32.Parse(question.Options.First().optionValue);
                                break;
                            case 'A':
                                nA += Int32.Parse(question.Options.First().optionValue);
                                break;
                            case 'N':
                                nN += Int32.Parse(question.Options.First().optionValue);
                                break;
                            default:
                                break;
                        }
                    }
                }
                double
                    isO = 14 + yO - nO,
                    isC = 14 + yC - nC,
                    isE = 20 + yE - nE,
                    isA = 14 + yA - nA,
                    isN = 14 + yN - nN;


                result1 = "O-" + (int)(isO / 40 * 100) + "%";
                result2 = "C-" + (int)(isC / 40 * 100) + "%";
                result3 = "E-" + (int)(isE / 40 * 100) + "%";
                result4 = "A-" + (int)(isA / 40 * 100) + "%";
                result5 = "N-" + (int)(isN / 40 * 100) + "%";

                

                if (userId != null)
                {
                    var attempt = CreateAttempt(request.TestId, userId, typeResult, result1, result2, result3, result4, result5, null);
                    foreach (var question in request.questions)
                    {
                        foreach (var option in question.Options)
                        {
                            var detail = CreateAttemptDetail(attempt.Result.Id, option.optionId, option.optionValue);

                        }

                    }
                    await _context.SaveChangesAsync();
                }
                SubmitResponse response = new()
                {
                    Result1 = result1,
                    Result2 = result2,
                    Result3 = result3,
                    Result4 = result4,
                    Result5 = result5
                };
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private List<IntensityIndex> checkGraphDisc (IntensityIndex isD, IntensityIndex isI, IntensityIndex isS, IntensityIndex isC)
        {
            List<IntensityIndex> list = new List<IntensityIndex>();   
            if (isD.Segment > 4)
            {
                list.Add(isD);
            }
            if (isI.Segment > 4)
            {
                list.Add(isI);
            }
            if (isS.Segment > 4)
            {
                list.Add(isS);
            }
            if (isC.Segment > 4)
            {
                list.Add(isC);
            }
            return list;
        } 
        public async Task<Test> UpdateTotalQuestion(int total, int testId)
        {
            try
            {
                var testDb = await _context.Tests
                    .Where(u => u.Id == testId)
                    .FirstOrDefaultAsync();
                if (testDb != null)
                {
                    testDb.TotalQuestion = total; 
                }
                else
                {
                    { throw new Exception("Test does not exist"); }
                }

                await _context.SaveChangesAsync();
                return testDb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
