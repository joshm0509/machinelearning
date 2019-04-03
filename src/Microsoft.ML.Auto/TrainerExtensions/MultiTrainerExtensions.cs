﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Trainers.LightGbm;

namespace Microsoft.ML.Auto
{
    using ITrainerEstimator = ITrainerEstimator<ISingleFeaturePredictionTransformer<object>, object>;

    internal class AveragedPerceptronOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new AveragedPerceptronBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildAveragePerceptronParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as AveragedPerceptronTrainer;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class FastForestOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new FastForestBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildFastForestParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as FastForestBinaryTrainer;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class LightGbmMultiExtension : ITrainerExtension
    {
        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildLightGbmParamsMulticlass();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            LightGbmMulticlassTrainer.Options options = TrainerExtensionUtil.CreateLightGbmOptions<LightGbmMulticlassTrainer.Options, VBuffer<float>, MulticlassPredictionTransformer<OneVersusAllModelParameters>, OneVersusAllModelParameters>(sweepParams, columnInfo);
            return mlContext.MulticlassClassification.Trainers.LightGbm(options);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildLightGbmPipelineNode(TrainerExtensionCatalog.GetTrainerName(this), sweepParams,
                columnInfo.LabelColumn, columnInfo.ExampleWeightColumn);
        }
    }

    internal class LinearSvmOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new LinearSvmBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildLinearSvmParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as LinearSvmTrainer;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class SdcaMaximumEntropyMultiExtension : ITrainerExtension
    {
        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildSdcaParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var options = TrainerExtensionUtil.CreateOptions<SdcaMaximumEntropyMulticlassTrainer.Options>(sweepParams, columnInfo.LabelColumn);
            return mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(options);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildPipelineNode(TrainerExtensionCatalog.GetTrainerName(this), sweepParams,
                columnInfo.LabelColumn);
        }
    }

    internal class LbfgsLogisticRegressionOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new LbfgsLogisticRegressionBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildLbfgsLogisticRegressionParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as LbfgsLogisticRegressionBinaryTrainer;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class SgdCalibratedOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new SgdCalibratedBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildSgdParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as SgdCalibratedTrainer;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class SymbolicSgdLogisticRegressionOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new SymbolicSgdLogisticRegressionBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return _binaryLearnerCatalogItem.GetHyperparamSweepRanges();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as SymbolicSgdLogisticRegressionBinaryTrainer;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class FastTreeOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new FastTreeBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildFastTreeParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as FastTreeBinaryTrainer;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class LbfgsMaximumEntropyMultiExtension : ITrainerExtension
    {
        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildLbfgsLogisticRegressionParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var options = TrainerExtensionUtil.CreateOptions<LbfgsMaximumEntropyMulticlassTrainer.Options>(sweepParams, columnInfo.LabelColumn);
            options.ExampleWeightColumnName = columnInfo.ExampleWeightColumn;
            return mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(options);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildPipelineNode(TrainerExtensionCatalog.GetTrainerName(this), sweepParams,
                columnInfo.LabelColumn, columnInfo.ExampleWeightColumn);
        }
    }
}