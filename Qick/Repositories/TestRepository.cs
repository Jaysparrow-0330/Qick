using Microsoft.EntityFrameworkCore;
using Qick.Dto.Enum;
using Qick.Dto.Requests;
using Qick.Dto.Responses;
using Qick.Models;
using Qick.Repositories.Interfaces;

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
                   ResultCareer = request.ResultCareer
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

        public async Task<SubmitResponse> CalculateTestResult(CalculateResultRequest request)
        {
            try
            {
                string typeResult = "";
                var type = await _context.Tests
                    .Where(i => i.Id == request.TestId)
                    .Include(u => u.TestType)
                    .FirstOrDefaultAsync();

                if (type.TestType.TestTypeName.Equals(TestTypeName.MBTI))
                {
                    
                    int
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
                    } 
                    else
                    {
                        typeResult = "E";
                    }
                    if (isN > isS)
                    {
                        typeResult += "N";
                    }
                    else
                    {
                        typeResult += "S";
                    }
                    if (isF > isT)
                    {
                        typeResult += "F";
                    }
                    else
                    {
                        typeResult += "T";
                    }
                    if (isJ > isP)
                    {
                        typeResult += "J";
                    }
                    else
                    {
                        typeResult += "P";
                    }
                    //if (isI > isE)
                    //{
                    //    typeResult.Insert(0,"I");
                    //}
                    //else if (isE >= isI)
                    //{
                    //    typeResult.Insert(0, "E");
                    //}

                    //if (isN > isS)
                    //{
                    //    typeResult.Insert(1, "N");
                    //}
                    //else if (isS >= isN)
                    //{
                    //    typeResult.Insert(1, "S");
                    //}

                    //if (isF > isT)
                    //{
                    //    typeResult.Insert(2, "F");
                    //}
                    //else if (isT >= isF)
                    //{
                    //    typeResult.Insert(2, "T");
                    //}

                    //if (isJ > isP)
                    //{
                    //    typeResult.Insert(3, "J");
                    //}
                    //else if (isP >= isJ)
                    //{
                    //    typeResult.Insert(3, "P");
                    //}


                }
                var result = getTestResult(typeResult, request.TestId);
                return result.Result;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        private async Task<SubmitResponse> getTestResult (string typeResult, int testId)
        {
            var result = await _context.Characters
                    .Where(a => a.ResultShortName.ToLower() == typeResult.ToLower())
                    .FirstOrDefaultAsync();

            SubmitResponse response = new()
            {
                Id = testId,
                ResultShortName = result.ResultShortName
            };
            return response;
        }
        public async Task<SubmitResponse> CalculateDiscResult(CalculateResultRequest request)
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
                var result = getTestResult(typeResult, request.TestId);
                return result.Result;
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
